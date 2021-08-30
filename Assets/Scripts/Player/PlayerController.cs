using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Ground")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheckRadius = .3f;
    [SerializeField] Vector3 gravity = new Vector3(0, -3f, 0);

    [Header("Speed")]
    [SerializeField] float speed = 0.5f;

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

    private void Update() {
        Move();

    }

    void FixedUpdate() {
        //CheckGround();
        Rotate();
        ApplyGravity();
    }

    /////////////////////////////////////////////Base controls
    public void OnMoveInput(Vector2 inputs) {
        xzMove = new Vector3(inputs.x, 0, inputs.y);
    }

    public void Constraint(bool _canMove, bool _canRotate) {
        canMove = _canMove;
        canRotate = _canRotate;
    }

    private void Rotate() {
        if (canRotate)
            LookAt(controller.transform.position + xzMove);
    }

    private void Move() {
        if (canMove) {
            //Get xzMove in local coordinates
            Vector3 moveDirection = transform.InverseTransformDirection(xzMove.normalized);

            animator.SetFloat("MoveForward", moveDirection.z);
            animator.SetFloat("MoveRight", moveDirection.x);

            moveMagnitude = Mathf.Clamp(xzMove.magnitude, 0, 1f);
            animator.SetFloat("InputMagnitude", moveMagnitude, animSmooth, Time.deltaTime);

            //Apply movement
            controller.Move(yMove * Time.deltaTime);
            controller.Move(xzMove.normalized * Time.deltaTime * speed);
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckRadius);
    }

}
