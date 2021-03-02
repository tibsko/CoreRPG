using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController player;
    public Joystick joystickMove;
    public Joystick joystickAim;
    public Interactable focus;

    private Transform transformInteractable;


    [SerializeField] Transform groundChecker;
    [SerializeField] Transform jumpZone;

    [SerializeField] float speedMove = 5f;
    [SerializeField] float radiusCheck = 2f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float autoShootRadius = 10f;
    [SerializeField] float radiusInteractable = 3f;
    [SerializeField] Vector3 gravity = new Vector3(0, -3f, 0);

    private PlayerWeapons playerWeapons;

    private Vector3 xyMove;
    public Vector3 xyAim;
    private Vector3 yVelocity = Vector3.zero;
    private Vector3 pointToShoot;

    private bool isGrounded = false;
    private bool isAutoShooting = false;

    private Rect aimJoystickZone;
    private Rect moveJoystickZone;

    public bool isShooting = false;
    public bool isAiming = false;
    public bool interactableDetected = false;


    private float distanceEnemy;
    private Vector3 enemyToShoot;

    private float timePressed = 0f;

    void Start()
    {
        player = GetComponent<CharacterController>();

        moveJoystickZone = new Rect(0, 0, Screen.width * 0.5f, Screen.height);
        aimJoystickZone = new Rect(Screen.width * 0.5f, 0, Screen.width, Screen.height*0.8f);

        playerWeapons = GetComponent<PlayerWeapons>();
    }

    void Update()
    {
        CheckGround();
        CheckBorderJump();
        JoystickMethods();
        DetectInteractable();

        #region Rotating aim
        //if (Input.GetKey(KeyCode.Mouse1)) {
        //    //Get mouse position in world space
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    RaycastHit hit;
        //    Physics.Raycast(ray, out hit, 100);
        //    //Look at
        //    player.transform.LookAt(new Vector3(hit.point.x, player.transform.position.y, hit.point.z));

        //}
        //else
        #endregion

        Rotate();

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //   Jump();
        //}

        //if (isGrounded && yVelocity.y < 0)
        //    yVelocity.y = -2f;
        //else
        yVelocity.y += gravity.y * Time.deltaTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void JoystickMethods()
    {
        foreach (Touch touch in Input.touches)
        {
            if (moveJoystickZone.Contains(touch.position) && isGrounded)
            {
                xyMove = new Vector3(joystickMove.Horizontal, 0f, joystickMove.Vertical);
            }
            else if (aimJoystickZone.Contains(touch.position))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    timePressed = Time.time;

                    distanceEnemy = 20f;

                    Collider[] colliders = Physics.OverlapSphere(transform.position, autoShootRadius);

                    foreach (var collider in colliders)
                    {
                        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        {
                            if (distanceEnemy > (collider.transform.position - player.transform.position).magnitude)
                            {
                                distanceEnemy = (collider.transform.position - player.transform.position).magnitude;
                                enemyToShoot = collider.transform.position;
                            }
                        }
                    }

                    pointToShoot = new Vector3(enemyToShoot.x, 0, enemyToShoot.z);
                    isAutoShooting = true;
                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    xyAim = new Vector3(joystickAim.Horizontal, 0, joystickAim.Vertical);
                    isShooting = false;
                    isAiming = true;
                    isAutoShooting = false;
                    pointToShoot = xyAim;
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    timePressed = Time.time - timePressed;
                    isShooting = true;
                    isAiming = false;


                }

            }
            else
            {
                xyMove = Vector3.zero;
                isAiming = false;
            }


        }
    }
    private void Move()
    {
        player.Move(xyMove * Time.fixedDeltaTime * speedMove);
        player.Move(yVelocity * Time.fixedDeltaTime);
    }

    public void Rotate()
    {
        if (!isShooting && !focus)
        {
            player.transform.LookAt(player.transform.position + xyMove);
        }
        else if (isShooting)
        {
            if (isAutoShooting)
            {
                player.transform.LookAt(new Vector3(0,player.transform.position.y,0)+pointToShoot);
                StartCoroutine(IsRotatingAim());
            }
            if (!isAutoShooting)
            {
                player.transform.LookAt(player.transform.position + xyAim);
                StartCoroutine(IsRotatingAim());
            }
        }
        else if (!isShooting && focus)
        {
            player.transform.LookAt(transformInteractable.position);
        }

    }

    IEnumerator IsRotatingAim()
    {
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
    }



    private void CheckBorderJump()
    {
        Collider[] colliders = Physics.OverlapSphere(jumpZone.position, radiusCheck, GameManager.instance.groundLayer);
        if (colliders.Length == 0 && isGrounded == true)
        {
            Jump();
        }
    }
    private void Jump()
    {
        yVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity.y);

    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, radiusCheck, GameManager.instance.groundLayer, QueryTriggerInteraction.Ignore);
    }

    void DetectInteractable()
    {
        int layerId = LayerMask.NameToLayer("Interactable");
        int layerMask = 1 << layerId;
        Collider[] interactablesDetected = Physics.OverlapSphere(player.transform.position, radiusInteractable, layerMask);
        if (interactablesDetected.Length > 0)
        {
            foreach (var collider in interactablesDetected)
            {
                Interactable interactable = interactablesDetected[0].GetComponent<Interactable>();
                transformInteractable = interactablesDetected[0].GetComponent<Transform>();
                SetFocus(interactable);
                Debug.Log("interactable detected");
            }
        }
        else
            RemoveFocus();
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus!=focus)
        {
            focus = newFocus;
            if(focus!=null)
                focus.OnDeFocused();
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(focus!=null)
            focus.OnDeFocused();
        focus = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(jumpZone.position, radiusCheck);
        Gizmos.DrawWireSphere(groundChecker.position, radiusCheck);
        Gizmos.DrawWireSphere(transform.position, autoShootRadius);
        Gizmos.DrawWireSphere(transform.position, radiusInteractable);
    }

}
