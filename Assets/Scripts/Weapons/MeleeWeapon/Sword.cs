using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon {
    private SwordData swordData;

    // Start is called before the first frame update
    void Start() {
        base.Start();

        if (this.MeleeWeaponData.GetType() == typeof(SwordData))
            swordData = MeleeWeaponData as SwordData;
        else {
            Debug.LogError("Wrong WeaponData Type in " + this.name);
        }
    }

    public override void Attack() {

    }

    public override bool CanAttack() {
        return true;
    }
}
