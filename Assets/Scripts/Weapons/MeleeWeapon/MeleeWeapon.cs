using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    [SerializeField] HitBox[] hitBoxes;

    public override bool CanAttack() {
        return true;
    }

    public override void Attack() {
    }

    protected void Start() {
    }

    public void ToggleHitBoxes(bool state) {
        Debug.Log($"Status : {state}");
        foreach (HitBox hitBox in hitBoxes) {
            hitBox.gameObject.SetActive(state);
        }
    }


}
