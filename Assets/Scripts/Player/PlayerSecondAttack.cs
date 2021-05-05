using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondAttack : MonoBehaviour
{
    private bool isAttacking = false;
    private bool startedAiming = false;
    private float distanceEnemy;

    private PlayerController controller;
    private Inventory inventory;

    public Vector3 targetPosition;

    //////////////////////////////////////////////PROPERTIES
    public Vector3 AimDirection { get; private set; }
    public bool DisplayAim { get => startedAiming; }
    //public SecondWeapon ActiveSecondWeapon { get { return inventory.ActiveSecondWeapon; } }


    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<PlayerController>();
        inventory = GetComponent<Inventory>();
        startedAiming = false;
    }

    // Update is called once per frame
    

    //public void OnAim(Vector2 aim) {
    //    //if (!ActiveSecondWeapon) {
    //    //    return;
    //    //}

    //    AimDirection = new Vector3(aim.x, 0, aim.y);

    //    if (AimDirection.magnitude > 0.5f) {
    //        startedAiming = true;
    //    }
    //    targetPosition = AimDirection * 3+new Vector3(0,transform.position.y,0);
    //}

    //public void OnRelease(Vector2 aim) {
    //    HandleShoot();
    //}

    //private void HandleShoot() {
    //    if (startedAiming && AimDirection.magnitude < 0.5f) {
    //        //Cancel shoot
    //        Debug.Log("Cancel shoot");
    //    }
    //    else if (!startedAiming && AimDirection.magnitude < 0.5f) {
    //        Debug.Log("Autoshooting");
    //        AutoShoot();
    //    }
    //    else if (startedAiming) {
    //        Debug.Log("Aiming shoot");
    //        AimShoot();
    //    }
    //    else {
    //        Debug.Log("Nothing");
    //    }
    //    startedAiming = false;

    //}

    //private void AimShoot() {
    //    controller.Constraint(true, false);
    //    Rotate(transform.position + AimDirection);
    //    Attack();
    //}

    //private void AutoShoot() {
    //    controller.Constraint(true, false);
    //    distanceEnemy = float.MaxValue;

    //    Collider[] colliders = Physics.OverlapSphere(transform.position, inventory.ActiveSecondWeapon.range);
    //    Vector3 target = transform.position + transform.forward;

    //    foreach (var collider in colliders) {
    //        if (1 << collider.gameObject.layer == LayerManager.instance.enemyLayer) {
    //            float distance = (collider.transform.position - transform.position).magnitude;
    //            if (distanceEnemy > distance) {
    //                distanceEnemy = distance;
    //                target = collider.transform.position;
    //            }
    //        }
    //    }
    //    Rotate(target);
    //    Attack();
    //}

    public void Rotate(Vector3 target) {
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    //private void Attack() {
    //    isAttacking = true;
    //    targetPosition = AimDirection * ActiveSecondWeapon.range + transform.position;
    //    SecondWeapon weapon = inventory.ActiveSecondWeapon;
    //    weapon.onEndAttack.RemoveAllListeners();
    //    weapon.onEndAttack.AddListener(ResetAttack);
    //    if (weapon.CanAttack()) {
    //        weapon.Attack();
    //        //SetAttackAnimation(isAttacking);
    //    }
    //}

    public void ResetAttack() {
        isAttacking = false;
        controller.Constraint(true, true);
        //SetAttackAnimation(isAttacking);
        //delay player can't attack ?
    }

}
