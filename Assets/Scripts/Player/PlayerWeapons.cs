using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject[] weaponPrefabs;
    [SerializeField] Transform weaponSlot;
    private int activeWeapon = 0;
    List<GameObject> equipedWeapons;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        equipedWeapons = new List<GameObject>();


        foreach (var weapon in weaponPrefabs)
        {
            GameObject go = Instantiate(weapon, weaponSlot.position, Quaternion.identity, weaponSlot);
            go.layer = LayerMask.NameToLayer("Player");
            equipedWeapons.Add(go);
        }
        EquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            activeWeapon++;
            EquipWeapon();
        }

        if (player.isShooting)
        {
            Shoot();
        }
        Weapon w = GetActiveWeapon();
        if (w.nbBulletsShooted >= w.weaponData.nbBulletToShoot)
        {
            player.isShooting = false;
            w.nbBulletsShooted = 0;
        }
    }

    public void EquipWeapon()
    {
        if (activeWeapon >= equipedWeapons.Count)
        {
            activeWeapon = 0;
        }
        DisplayWeapon();
        ChangeAnimation(GetActiveWeapon().weaponData.overideAnimator);
    }
    void DisplayWeapon()
    {
        for (int i = 0; i < equipedWeapons.Count; i++)
        {
            equipedWeapons[i].SetActive(false);
        }
        equipedWeapons[activeWeapon].SetActive(true);
    }

    public void AimAndShoot(float rotation)
    {

    }

    public void Shoot()
    {
        Weapon weapon = GetActiveWeapon();
        if (weapon && weapon.CanShoot())
        {
            animator.SetTrigger("Attack");
            weapon.Shoot();
        }
    }

    private Weapon GetActiveWeapon()
    {
        return equipedWeapons[activeWeapon].GetComponent<Weapon>();
    }

    private void ChangeAnimation(AnimatorOverrideController animatorOverrideController)
    {
        animator.runtimeAnimatorController = animatorOverrideController;
    }
}
