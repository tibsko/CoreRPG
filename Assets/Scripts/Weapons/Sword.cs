using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    private SwordData swordData;

    // Start is called before the first frame update
    void Start() {
        base.Start();

        if (weaponData.GetType() == typeof(SwordData))
            swordData = weaponData as SwordData;
        else {
            Debug.LogError("Wrong WeaponData Type in " + this.name);
        }
    }

    public override void Shoot() {

    }

    public override bool CanShoot() {
        return true;
    }
}
