using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

    [SerializeField] Weapon[] weaponPrefabs;
    [SerializeField] Transform weaponSlot;
    [SerializeField] bool activateHitBoxes = false;

    //private public bool isShooting = false;
    //private bool isAutoShooting = false;
    //private float timePressed = 0f;


    public Vector3 AimDirection { get; private set; }
    public bool DisplayAim { get; private set; }

    private bool startedAiming;
    private float distanceEnemy;
    private Animator animator;
    private int activeWeaponIndex = 0;
    private List<Weapon> equipedWeapons;
    private PlayerController controller;

    public Weapon ActiveWeapon { get { return equipedWeapons[activeWeaponIndex]; } }

    // Start is called before the first frame update
    void Start() {
        //animator = GetComponentInChildren<Animator>();
        equipedWeapons = new List<Weapon>();
        controller = GetComponent<PlayerController>();
        startedAiming = false;

        //aimJoystickZone = new Rect(Screen.width * 0.5f, 0, Screen.width, Screen.height * 0.8f);

        foreach (var weaponPrefab in weaponPrefabs) {
            GameObject go = Instantiate(weaponPrefab.gameObject, weaponSlot.position, Quaternion.identity, weaponSlot);
            go.layer = gameObject.layer;

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

    public void NextWeapon() {
        activeWeaponIndex++;
        if (activeWeaponIndex >= equipedWeapons.Count) {
            activeWeaponIndex = 0;
        }
        EquipWeapon();
    }

    public void EquipWeapon() {
        DisplayActiveWeapon();
        ChangeAnimation(GetActiveWeapon().GetAnimatorOverride());
    }

    void DisplayActiveWeapon() {
        for (int i = 0; i < equipedWeapons.Count; i++) {
            equipedWeapons[i].gameObject.SetActive(false);
        }
        equipedWeapons[activeWeaponIndex].gameObject.SetActive(true);
    }

    private void AimShoot() {
        Rotate(transform.position + AimDirection);
        Fire();
    }

    private void AutoShoot() {
        distanceEnemy = 20f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, ActiveWeapon.autoshootDistance);
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
        controller.LookAt(target);
    }


    private IEnumerator LockRotation() {
        controller.RotationIsLocked = true;
        yield return new WaitForSeconds(0.5f);
        controller.RotationIsLocked = false;
    }

    private void Fire() {
        Weapon weapon = GetActiveWeapon();
        if (weapon && weapon.CanAttack()) {
            animator.SetTrigger("Attack");
            animator.SetInteger("Combos", weapon.combosCount);
            weapon.Attack();
        }
        DisplayAim = false;
    }

    public void ToggleHitBox(bool state) {
        Debug.Log(ActiveWeapon.GetType().ToString());
        if (ActiveWeapon is MeleeWeapon) {
            MeleeWeapon meleeWeapon = ActiveWeapon as MeleeWeapon;
            meleeWeapon.ToggleHitBoxes(state);
        }
    }


    public Weapon GetActiveWeapon() {
        return equipedWeapons[activeWeaponIndex].GetComponent<Weapon>();
    }

    private void ChangeAnimation(AnimatorOverrideController animatorOverrideController) {
        //animator.runtimeAnimatorController = animatorOverrideController;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (equipedWeapons != null && equipedWeapons.Count > 0 && equipedWeapons[activeWeaponIndex])
            Gizmos.DrawWireSphere(transform.position, ActiveWeapon.autoshootDistance);
    }
}
