using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    [SerializeField] HitBoxSword[] hitBoxes;
    public MeleeWeaponData MeleeWeaponData { get { return weaponData as MeleeWeaponData; } }


    public override bool CanAttack() {
        return true;
    }

    public override void Attack() {
    }

    protected void Start() {
        base.Start();
    }

    public void ToggleHitBoxes(bool state) {
        Debug.Log($"Status : {state}");
        foreach (HitBoxSword hitBox in hitBoxes) {
            hitBox.gameObject.SetActive(state);
        }
    }


}
