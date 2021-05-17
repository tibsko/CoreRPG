using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    //////////////////////////////////////////////INSPECTOR

    [SerializeField] bool activateHitBoxes = false;


    //////////////////////////////////////////////PRIVATE

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

    public void OnAim(Vector2 aim) {
        if (!ActiveWeapon) {
            return;
        }

        AimDirection = new Vector3(aim.x, 0, aim.y);

        if (AimDirection.magnitude > 0.5f) {
            startedAiming = true;
        }
    }

    public void OnRelease(Vector2 aim) {
        HandleAttack();
    }

    private void HandleAttack() {
        if (startedAiming && AimDirection.magnitude < 0.5f) {
            //Cancel shoot
            Debug.Log("Cancel shoot");
        }
        else if (!startedAiming && AimDirection.magnitude < 0.5f) {
            Debug.Log("Autoshooting");
            AutoAttack();
        }
        else if (startedAiming) {
            Debug.Log("Aiming shoot");
            AimedAttack();
        }
        else {
            Debug.Log("Nothing");
        }
        startedAiming = false;

    }

    private void AimedAttack() {
        controller.Constraint(true, false);
        Rotate(transform.position + AimDirection);
        Attack();
    }

    private void AutoAttack() {
        controller.Constraint(true, false);
        distanceEnemy = float.MaxValue;

        Collider[] colliders = Physics.OverlapSphere(transform.position, playerWeapons.ActiveWeapon.autoshootDistance);
        Vector3 target = transform.position + transform.forward;

        foreach (var collider in colliders) {
            if (ReferenceManager.instance.enemyLayer.ContainsLayer(collider.gameObject.layer)) {
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
        if (weapon.CanAttack()) {
            weapon.Attack();
            animator.SetBool("IsStrafing", true);
        }
    }

    public void ResetAttack() {
        controller.Constraint(true, true);
        animator.SetBool("IsStrafing", false);
        //delay player can't attack ?
    }

    public void ToggleHitBox(int state) {
        if (playerWeapons.ActiveWeapon is MeleeWeapon) {
            MeleeWeapon meleeWeapon = playerWeapons.ActiveWeapon as MeleeWeapon;
            meleeWeapon.ToggleHitBoxes(state == 1);
        }
    }
    private void OnDrawGizmos() {
        //Gizmos.color = Color.red;
        //if (playerWeapons.equipedWeapons != null && playerWeapons.equipedWeapons.Count > 0 && playerWeapons.equipedWeapons[activeWeaponIndex])
        //    Gizmos.DrawWireSphere(transform.position, playerWeapons.ActiveWeapon.autoshootDistance);
    }
}
