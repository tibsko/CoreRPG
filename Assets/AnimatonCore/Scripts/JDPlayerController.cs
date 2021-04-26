using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JDPlayerController : MonoBehaviour {

    [Header("Ground")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheckRadius = .3f;
    [SerializeField] Vector3 gravity = new Vector3(0, -3f, 0);

    [Header("Speed")]
    [SerializeField] float forwardSpeed = 0.5f;
    [SerializeField] float sideSpeed = 1;
    [SerializeField] float backwardSpeed = 1;
    [SerializeField] float walkFactor = 0.5f;
    [SerializeField] float sprintFactor = 1.5f;
    [SerializeField] float animSmooth = 0.5f;

    [Header("Joystick")]
    [SerializeField] RectTransform joystickBase;
    [SerializeField] RectTransform joystickHandle;

    public bool IsGrounded { get; private set; }
    public bool RotationIsLocked { get; set; }

    private CharacterController controller;
    private Vector3 xzMove = Vector3.zero;
    private Vector3 yMove = Vector3.zero;
    private AnimatorBridge animator;

    private float moveMagnitude = 0;

    private Vector3 joystickStart = Vector3.zero;
    private Vector3 aimPoint = Vector3.zero;
    private bool inMelee = false;
    private bool isAiming = false;
    private LineRenderer line;


    void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<AnimatorBridge>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        ToggleJoystick(false);
    }

    public JDFireWeapon weapon;
    void Update() {
        OnMoveInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        CheckGround();
        Rotate();
        Interact();
        Aim();
        Attack();
        ApplyGravity();
        Move();

        //float diff = (joystickHandle.position - joystickBase.position).magnitude;
        //weapon.shooting = diff > 50;
    }

    /////////////////////////////////////////////Base controls
    public void OnMoveInput(Vector2 inputs) {
        xzMove = new Vector3(inputs.x, 0, inputs.y);
    }

    private void Rotate() {
        if (isAiming && (aimPoint - transform.position).magnitude > 0.2f) {
            LookAt(aimPoint);
        }
        else if (!isAiming && inMelee) {
            LookAt(aimPoint);
        }
        else
            LookAt(controller.transform.position + xzMove);
    }

    private void Move() {
        Vector3 moveDirection = transform.InverseTransformDirection(xzMove.normalized);
        animator.SetFloat("MoveForward", moveDirection.z);
        animator.SetFloat("MoveRight", moveDirection.x);

        float velocity = 0f;
        velocity += backwardSpeed * Mathf.Clamp(moveDirection.z, -1, 0);
        velocity += forwardSpeed * Mathf.Clamp(moveDirection.z, 0, 1);
        velocity += sideSpeed * moveDirection.x;

        #region debug
        //string debugVelocity = "";
        //debugVelocity += "Backward=" + backwardSpeed * Mathf.Clamp(v.z, -1, 0);
        //debugVelocity += "  Forward=" + forwardSpeed * Mathf.Clamp(v.z, 0, 1);
        //debugVelocity += "  Side=" + sideSpeed * v.x;
        //Debug.Log(debugVelocity);
        #endregion

        float factor = 1;
        if (Input.GetKey(KeyCode.CapsLock)) factor = walkFactor;
        else if (Input.GetKey(KeyCode.LeftShift)) factor = sprintFactor;

        velocity *= factor;
        moveMagnitude = Mathf.Clamp(xzMove.magnitude, 0, 1f) * factor;
        animator.SetFloat("InputMagnitude", moveMagnitude, animSmooth, Time.deltaTime);

        //Apply movement
        controller.Move(yMove * Time.deltaTime);
        controller.Move(xzMove.normalized * Time.deltaTime * Mathf.Abs(velocity));
    }

    private void ApplyGravity() {
        //Apply gravity
        if (IsGrounded && yMove.y < 0)
            yMove.y = -2f;
        else
            yMove.y += gravity.y * Time.deltaTime;
    }


    /////////////////////////////////////////////Actions

    private void Interact() {
        if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetTrigger("Interaction");
            //animator.SetInteger("InteractionID",1);
        }
    }

    private void Aim() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            isAiming = true;
            line.enabled = true;
            ToggleJoystick(true);
            joystickStart = Input.mousePosition;
            joystickBase.position = Input.mousePosition;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) {
            isAiming = false;
            line.enabled = false;
            ToggleJoystick(false);
            joystickStart = Vector3.zero;
        }

        if (isAiming) {
            joystickHandle.position = Input.mousePosition;
            Vector3 aimVect = (Input.mousePosition - joystickStart).normalized;
            aimPoint = transform.position + new Vector3(aimVect.x, 0, aimVect.y) * 5;

            line.SetPosition(0, transform.position + Vector3.up * 0.1f);
            line.SetPosition(1, aimPoint + Vector3.up * 0.1f);
            //Debug.DrawLine(transform.position + Vector3.up * 1f, aimPoint + Vector3.up * 1f, Color.cyan);
        }
        animator.SetBool("IsAiming", isAiming);
        animator.SetBool("IsStrafing", isAiming || inMelee);
    }

    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Mouse2)) {
            animator.SetTrigger("MeleeAttack");
        }
    }

    ////////////////////////////////////////////Utilities
    private void ToggleJoystick(bool state) {
        joystickBase.gameObject.SetActive(state);
        joystickHandle.gameObject.SetActive(state);
    }

    private void LookAt(Vector3 target) {
        target.y = transform.position.y;
        controller.transform.LookAt(target);
    }

    private void CheckGround() {
        IsGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, LayerMask.NameToLayer("Default"), QueryTriggerInteraction.Ignore);
    }

    ////////////////////////////////////////////Animation tools

    public void OnEnterAttack() {
        inMelee = true;
        aimPoint = transform.position + transform.forward * 100;
    }

    public void OnLeaveAttack() {
        inMelee = false;
    }

    //Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckRadius);
    }
}
