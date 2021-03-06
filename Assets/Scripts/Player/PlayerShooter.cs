using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

    [SerializeField] GameObject[] weaponPrefabs;
    [SerializeField] Transform weaponSlot;

    [SerializeField] float autoShootRadius = 10f;

    //private Rect aimJoystickZone;
    //public Joystick joystickAim;

    public bool isShooting = false;
    public bool isAiming = false;
    public Vector3 xzAim;
    private Vector3 pointToShoot;
    private bool isAutoShooting = false;
    private float distanceEnemy;
    private Vector3 enemyToShoot;
    private float timePressed = 0f;

    private Animator animator;
    private int activeWeaponIndex = 0;
    private List<Weapon> equipedWeapons;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        equipedWeapons = new List<Weapon>();
        controller = GetComponent<PlayerController>();

        //aimJoystickZone = new Rect(Screen.width * 0.5f, 0, Screen.width, Screen.height * 0.8f);

        foreach (var weaponPrefab in weaponPrefabs) {
            GameObject go = Instantiate(weaponPrefab, weaponSlot.position, Quaternion.identity, weaponSlot);
            go.layer = LayerManager.instance.playerLayer;

            Weapon weapon = go.GetComponent<Weapon>();
            if (weapon) {
                equipedWeapons.Add(weapon);
            }
            else
                Debug.LogError($"Weapon script is missing on {go.name}");
        }
        EquipWeapon();
    }

    // Update is called once per frame
    void Update() {

        HandleShoot();

        //if (isShooting) {
        //    Shoot();
        //}
        //Weapon w = GetActiveWeapon();
        //if (w.nbBulletsShooted >= w.weaponData.nbBulletToShoot) {
        //    isShooting = false;
        //    w.nbBulletsShooted = 0;
        //}
    }

    private void HandleShoot() {

    }

    //private void JoystickMethods() {
    //    foreach (Touch touch in Input.touches) {
    //        if (moveJoystickZone.Contains(touch.position) && isGrounded) {
    //            xyMove = new Vector3(joystickMove.Horizontal, 0f, joystickMove.Vertical);
    //        }
    //        else if (aimJoystickZone.Contains(touch.position)) {
    //            if (touch.phase == TouchPhase.Began) {

    //            }

    //            else if (touch.phase == TouchPhase.Moved) {
    //                xzAim = new Vector3(joystickAim.Horizontal, 0, joystickAim.Vertical);
    //                if (xzAim.magnitude < .7f) {
    //                    AutoShoot();
    //                }
    //                else {
    //                    isShooting = false;
    //                    isAiming = true;
    //                    isAutoShooting = false;
    //                    pointToShoot = xzAim;
    //                }
    //            }

    //            else if (touch.phase == TouchPhase.Ended) {
    //                timePressed = Time.time - timePressed;
    //                isShooting = true;
    //                if (!isAiming) {
    //                    AutoShoot();
    //                }
    //                isAiming = false;

    //            }

    //        }
    //        else {
    //            xyMove = Vector3.zero;
    //            isAiming = false;
    //        }
    //    }
    //}

    public void NextWeapon() {
        activeWeaponIndex++;
        if (activeWeaponIndex >= equipedWeapons.Count) {
            activeWeaponIndex = 0;
        }
        EquipWeapon();
    }

    public void EquipWeapon() {
        DisplayActiveWeapon();
        ChangeAnimation(GetActiveWeapon().weaponData.overideAnimator);
    }

    void DisplayActiveWeapon() {
        for (int i = 0; i < equipedWeapons.Count; i++) {
            equipedWeapons[i].gameObject.SetActive(false);
        }
        equipedWeapons[activeWeaponIndex].gameObject.SetActive(true);
    }

    public void Shoot() {
        Weapon weapon = GetActiveWeapon();
        if (weapon && weapon.CanShoot()) {
            animator.SetTrigger("Attack");
            weapon.Shoot();
        }
    }

    private void AutoShoot() {
        distanceEnemy = 20f;
        isAiming = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, autoShootRadius);

        foreach (var collider in colliders) {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                if (distanceEnemy > (collider.transform.position - transform.position).magnitude) {
                    distanceEnemy = (collider.transform.position - transform.position).magnitude;
                    enemyToShoot = collider.transform.position;
                }
            }
        }

        pointToShoot = new Vector3(enemyToShoot.x, 0, enemyToShoot.z);
        isAutoShooting = true;
    }

    public void Rotate() {
        //if (!isShooting && !focus) {
        //    transform.LookAt(transform.position + xyMove);
        //}
        //else if (isShooting) {
        //    if (isAutoShooting) {
        //        transform.LookAt(new Vector3(0, transform.position.y, 0) + pointToShoot);
        //        StartCoroutine(IsRotatingAim());
        //    }
        //    if (!isAutoShooting) {
        //        transform.LookAt(transform.position + xzAim);
        //        StartCoroutine(IsRotatingAim());
        //    }
        //}
        //else if (!isShooting && focus) {
        //    transform.LookAt(transformInteractable.position);
        //}

    }


    private IEnumerator IsRotatingAim() {
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
    }


    public Weapon GetActiveWeapon() {
        return equipedWeapons[activeWeaponIndex].GetComponent<Weapon>();
    }

    private void ChangeAnimation(AnimatorOverrideController animatorOverrideController) {
        animator.runtimeAnimatorController = animatorOverrideController;
    }
}
