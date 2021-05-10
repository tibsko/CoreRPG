using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour {

    [SerializeField] int nbMaxWeapon = 5;
    [SerializeField] Weapon[] weaponPrefabs;
    [SerializeField] Transform fireWeaponSlot;
    [SerializeField] Transform meleeWeaponSlot;

    public List<Weapon> equipedWeapons;
    public int ActiveWeaponIndex { get; private set; }

    public Weapon ActiveWeapon { get { return equipedWeapons[ActiveWeaponIndex]; } }

    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        equipedWeapons = new List<Weapon>();
        animator = GetComponent<Animator>();

        foreach (var weaponPrefab in weaponPrefabs) {
            EquipWeapon(weaponPrefab);
        }

        DisplayActiveWeapon();
    }

    public void AddWeapon(Weapon weapon) {

        if (equipedWeapons.Count >= nbMaxWeapon) {
            Weapon activeWeapon = ActiveWeapon;
            RemoveWeapon(activeWeapon);
        }
        EquipWeapon(weapon);
        DisplayActiveWeapon();
    }

    public void RemoveWeapon(Weapon weapon) {
        equipedWeapons.Remove(weapon);
        Destroy(weapon.gameObject);
    }


    public void NextWeapon() {
        if (!ActiveWeapon.IsAttacking) {
            ActiveWeaponIndex++;
            if (ActiveWeaponIndex >= equipedWeapons.Count) {
                ActiveWeaponIndex = 0;
            }
            DisplayActiveWeapon();
        }
    }

    public void EquipWeapon(Weapon weapon) {

        var type = weapon.GetType();
        Transform weaponSlot;
        if (type == typeof(FireWeapon))
            weaponSlot = fireWeaponSlot;
        else 
            weaponSlot = meleeWeaponSlot;

        GameObject go = Instantiate(weapon.gameObject, weaponSlot);
        go.SetActive(false);
        go.layer = gameObject.layer;
        equipedWeapons.Add(go.GetComponent<Weapon>());
        ActiveWeaponIndex = equipedWeapons.Count - 1;   //TODO : this always select last weapon, wrong behaviour
    }

    void DisplayActiveWeapon() {
        for (int i = 0; i < equipedWeapons.Count; i++) {
            equipedWeapons[i].gameObject.SetActive(false);
        }
        equipedWeapons[ActiveWeaponIndex].gameObject.SetActive(true);
        ChangeAnimation(ActiveWeapon.overrideAnimator);
    }
    private void ChangeAnimation(AnimatorOverrideController animatorOverrideController) {
        //animator.runtimeAnimatorController = animatorOverrideController;
    }
}
