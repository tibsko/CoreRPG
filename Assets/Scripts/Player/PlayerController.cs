using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Transform groundChecker;
    //[SerializeField] Transform jumpZone;

    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float groundCheckRadius = 2f;
    [SerializeField] float radiusInteractable = 3.5f;
    [SerializeField] Vector3 gravity = new Vector3(0, -3f, 0);

    public bool IsGrounded { get; private set; }
    public bool RotationIsLocked { get; set; }
    private Interactable interactable;

    private CharacterController controller;
    private Interactable focus;
    private Vector3 xzMove = Vector3.zero;
    private Vector3 yMove = Vector3.zero;
    private Animator animator;

    void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        CheckGround();
        CheckBorderJump();
        DetectInteractable();
        Rotate();

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //   Jump();
        //}
        
        //Apply gravity
        if (IsGrounded && yMove.y < 0)
            yMove.y = -2f;
        else
            yMove.y += gravity.y * Time.deltaTime;

    }

    private void FixedUpdate() {
        Move();
    }

    public void OnMoveInput(Vector2 inputs) {
        xzMove = new Vector3(inputs.x, 0, inputs.y);
    }

    private void Move() {
        controller.Move(Vector3.ClampMagnitude(xzMove * Time.fixedDeltaTime * maxSpeed, Time.fixedDeltaTime * maxSpeed));
        controller.Move(yMove * Time.fixedDeltaTime);
        float speed = xzMove.magnitude * 0.8f;
        animator.SetFloat("Speed", xzMove.magnitude);
    }

    private void Rotate() {
        if (!RotationIsLocked) {
            LookAt(controller.transform.position + xzMove);
        }
        else {
            //LookAt(focus.transform.position);
        }
    }

    public void LookAt(Vector3 target) {
        target.y = transform.position.y;
        controller.transform.LookAt(target);
    }

    private void CheckBorderJump() {
        //Collider[] colliders = Physics.OverlapSphere(jumpZone.position, groundCheckRadius, LayerManager.instance.groundLayer);
        //if (colliders.Length == 0 && IsGrounded == true) {
        //    Jump();
        //}
    }

    private void CheckGround() {
        IsGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, LayerManager.instance.groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void DetectInteractable() {
        Collider[] interactablesDetected = Physics.OverlapSphere(controller.transform.position, radiusInteractable, LayerManager.instance.interactableLayer);
        if (interactablesDetected.Length > 0) {
            foreach (var collider in interactablesDetected) {
                Interactable interactable = interactablesDetected[0].GetComponent<Interactable>();
                SetFocus(interactable);
            }
        }
        else
            RemoveFocus();
    }

    private void SetFocus(Interactable newFocus) {
        if (newFocus && newFocus != focus) {
            focus = newFocus;
            newFocus.OnFocused(transform);
            interactable = newFocus;
        }
    }

    private void RemoveFocus() {
        if (focus != null)
            focus.OnDeFocused();
        focus = null;

    }


    public void Interaction() {
        interactable.Interact(gameObject);
        //if (interactable.GetType()!=type.DoorInteractable)
            //interactable = null;
    }

    public void HoldDownInteraction() {
        interactable.HoldDownInteract();
    }
    public void HoldupInteraction() {
        interactable.HoldUpInteract();

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(jumpZone.position, groundCheckRadius);
        //Gizmos.DrawWireSphere(groundChecker.position, groundCheckRadius);
        //Gizmos.DrawWireSphere(transform.position, radiusInteractable);
    }

}
