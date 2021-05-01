using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    //////////////////////////////////////////////INSPECTOR

    [SerializeField] bool activateHitBoxes = false;


    //////////////////////////////////////////////PRIVATE

    private bool isAttacking = false;
    private bool startedAiming = false;
    private float distanceEnemy;

    private Animator animator;
    private PlayerController controller;
    private PlayerWeapons playerWeapons;

    //////////////////////////////////////////////PROPERTIES
    public Vector3 AimDirection { get; private set; }
    public bool DisplayAim { get => startedAiming; }
    public Weapon ActiveWeapon { get { return playerWeapons.ActiveWeapon; } }


    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        playerWeapons = GetComponent<PlayerWeapons>();
        startedAiming = false;
    }

    // Update is called once per frame
    void Update() {

        ToggleHitBox(activateHitBoxes);
    }

    public void OnAim(Vector2 aim) {

        AimDirection = new Vector3(aim.x, 0, aim.y);

        if (AimDirection.magnitude > 0.5f) {
            startedAiming = true;
        }

        animator.SetBool("IsAiming", isAttacking);
        animator.SetBool("IsStrafing", isAttacking);
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
            Debug.Log("Autoshooting");
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
        isAttacking = true;
        controller.Constraint(true, false);
        Rotate(transform.position + AimDirection);
        Attack();
    }

    private void AutoShoot() {
        isAttacking = true;
        controller.Constraint(true, false);
        distanceEnemy = float.MaxValue;

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
        Attack();
    }

    public void Rotate(Vector3 target) {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    private void Attack() {
        Weapon weapon = playerWeapons.ActiveWeapon;
        weapon.onEndAttack.RemoveAllListeners();
        weapon.onEndAttack.AddListener(ResetAttack);
        if (weapon && weapon.CanAttack()) {
            animator.SetInteger("Combos", weapon.combosCount);
            weapon.Attack();
        }
    }

    public void ResetAttack() {
        isAttacking = false;
        controller.Constraint(true, true);

        //delay player can't attack ?
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
