using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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

    public bool IsGrounded { get; private set; }

    private CharacterController controller;
    private Vector3 xzMove = Vector3.zero;
    private Vector3 yMove = Vector3.zero;
    private AnimatorBridge animator;
    private float moveMagnitude = 0;

    private bool canMove = true;
    private bool canRotate = true;

    void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<AnimatorBridge>();
    }

    void Update() {
        CheckGround();
        Rotate();
        ApplyGravity();
        Move();
    }

    /////////////////////////////////////////////Base controls
    public void OnMoveInput(Vector2 inputs) {
        xzMove = new Vector3(inputs.x, 0, inputs.y);
    }

    public void Constraint(bool movement, bool rotation) {
        canMove = movement;
        canRotate = rotation;
    }

    private void Rotate() {
        if (canRotate)
            LookAt(controller.transform.position + xzMove);
    }

    private void Move() {
        if (canMove) {
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
            #region debug
            if (Input.GetKey(KeyCode.CapsLock)) factor = walkFactor;
            else if (Input.GetKey(KeyCode.LeftShift)) factor = sprintFactor;
            #endregion
            velocity *= factor;
            moveMagnitude = Mathf.Clamp(xzMove.magnitude, 0, 1f) * factor;
            animator.SetFloat("InputMagnitude", moveMagnitude, animSmooth, Time.deltaTime);

            //Apply movement
            controller.Move(yMove * Time.deltaTime);
            controller.Move(xzMove.normalized * Time.deltaTime * Mathf.Abs(velocity));
        }
        else {
            animator.SetFloat("MoveForward", 0);
            animator.SetFloat("MoveRight", 0);
            animator.SetFloat("InputMagnitude", 0);
        }
    }

    private void ApplyGravity() {
        //Apply gravity
        if (IsGrounded && yMove.y < 0)
            yMove.y = -2f;
        else
            yMove.y += gravity.y * Time.deltaTime;
    }

    ////////////////////////////////////////////Utilities
    private void LookAt(Vector3 target) {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    private void CheckGround() {
        IsGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, LayerMask.NameToLayer("Default"), QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmos() {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(groundChecker.position, groundCheckRadius);
    }

}
