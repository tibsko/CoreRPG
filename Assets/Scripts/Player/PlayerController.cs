using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Transform groundChecker;
    [SerializeField] Transform jumpZone;

    [SerializeField] float speedMove = 5f;
    [SerializeField] float groundCheckRadius = 2f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float radiusInteractable = 3f;
    [SerializeField] Vector3 gravity = new Vector3(0, -3f, 0);

    public bool IsGrounded { get; private set; }
    public bool RotationIsLocked { get; set; }

    private CharacterController controller;
    private Interactable focus;
    private Vector3 xzMove = Vector3.zero;
    private Vector3 yMove = Vector3.zero;


    void Start() {
        controller = GetComponent<CharacterController>();

    }

    void Update() {
        CheckGround();
        CheckBorderJump();
        //DetectInteractable();
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
        controller.Move(Vector3.ClampMagnitude(xzMove * Time.fixedDeltaTime * speedMove, Time.fixedDeltaTime * speedMove));
        controller.Move(yMove * Time.fixedDeltaTime);
    }

    private void Rotate() {
        if (!RotationIsLocked  && !focus) {
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
        Collider[] colliders = Physics.OverlapSphere(jumpZone.position, groundCheckRadius, LayerManager.instance.groundLayer);
        if (colliders.Length == 0 && IsGrounded == true) {
            Jump();
        }
    }
    private void Jump() {
        yMove.y = Mathf.Sqrt(jumpForce * -2f * gravity.y);

    }

    private void CheckGround() {
        IsGrounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, LayerManager.instance.groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void DetectInteractable() {
        int layerId = LayerManager.instance.interactableLayer;
        int layerMask = 1 << layerId;
        Collider[] interactablesDetected = Physics.OverlapSphere(controller.transform.position, radiusInteractable, layerMask);
        if (interactablesDetected.Length > 0) {
            foreach (var collider in interactablesDetected) {
                Interactable interactable = interactablesDetected[0].GetComponent<Interactable>();
                SetFocus(interactable);
                Debug.Log("interactable detected");
            }
        }
        else
            RemoveFocus();
    }

    private void SetFocus(Interactable newFocus) {
        if (newFocus && newFocus != focus) {
            focus = newFocus;
            if (focus != null)
                focus.OnDeFocused();
        }
    }

    private void RemoveFocus() {
        if (focus != null)
            focus.OnDeFocused();
        focus = null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(jumpZone.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckRadius);
        Gizmos.DrawWireSphere(transform.position, radiusInteractable);
    }

}
