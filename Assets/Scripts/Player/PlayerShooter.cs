using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

    [SerializeField] bool activateHitBoxes = false;

    public Vector3 AimDirection { get; private set; }
    public bool DisplayAim { get; private set; }

    private bool startedAiming;
    private float distanceEnemy;
    private Animator animator;
    private int activeWeaponIndex = 0;
    private PlayerController controller;
    private PlayerWeapons playerWeapons;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController>();
        startedAiming = false;
        playerWeapons = gameObject.GetComponent<PlayerWeapons>();

    }

    // Update is called once per frame
    void Update() {

        ToggleHitBox(activateHitBoxes);

        //if (isShooting) {
        //    Shoot();
        //}
        //Weapon w = GetActiveWeapon();
        //if (w.nbBulletsShooted >= w.weaponData.nbBulletToShoot) {
        //    isShooting = false;
        //    w.nbBulletsShooted = 0;
        //}
    }

    public void OnAim(Vector2 aim) {
        AimDirection = new Vector3(aim.x, 0, aim.y);

        if (AimDirection.magnitude > 0.5f) {
            startedAiming = true;
            DisplayAim = true;
        }
        else
            DisplayAim = false;
    }

    public void OnRelease(Vector2 aim) {
        HandleShoot();
    }

    private void HandleShoot() {
        if (startedAiming && AimDirection.magnitude < 0.5f) {
            //Cancel shoot
            Debug.Log("Cancel shoot");
        }
        else if (!startedAiming && AimDirection.magnitude < 0.5f) {
            //Debug.Log("Autoshooting");
            AutoShoot();
        }
        else if (startedAiming) {
            Debug.Log("Aiming shoot");
            AimShoot();
        }
        else {
            Debug.Log("Nothing");
        }
        startedAiming = false;

    }

    private void AimShoot() {
        Rotate(transform.position + AimDirection);
        Fire();
    }

    private void AutoShoot() {
        distanceEnemy = 20f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, playerWeapons.ActiveWeapon.autoshootDistance);
        Vector3 target = transform.position + transform.forward;

        foreach (var collider in colliders) {
            if (1 << collider.gameObject.layer == LayerManager.instance.enemyLayer) {
                float distance = (collider.transform.position - transform.position).magnitude;
                if (distanceEnemy > distance) {
                    distanceEnemy = distance;
                    target = collider.transform.position;
                }
            }
        }
        Rotate(target);
        Fire();
        StartCoroutine(LockRotation());
    }

    public void Rotate(Vector3 target) {
        //controller.LookAt(target);
    }


    private IEnumerator LockRotation() {
        controller.ControlRotation = true;
        yield return new WaitForSeconds(0.5f);
        controller.ControlRotation = false;
    }

    private void Fire() {
        Weapon weapon = playerWeapons.GetActiveWeapon();
        if (weapon && weapon.CanAttack()) {
            animator.SetTrigger("Attack");
            animator.SetInteger("Combos", weapon.combosCount);
            weapon.Attack();
        }
        DisplayAim = false;
    }

    public void ToggleHitBox(bool state) {
        if (playerWeapons.ActiveWeapon is MeleeWeapon) {
            MeleeWeapon meleeWeapon = playerWeapons.ActiveWeapon as MeleeWeapon;
            meleeWeapon.ToggleHitBoxes(state);
        }
    }

    private void OnDrawGizmos() {
        //Gizmos.color = Color.red;
        //if (playerWeapons.equipedWeapons != null && playerWeapons.equipedWeapons.Count > 0 && playerWeapons.equipedWeapons[activeWeaponIndex])
        //    Gizmos.DrawWireSphere(transform.position, playerWeapons.ActiveWeapon.autoshootDistance);
    }
}
