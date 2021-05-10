using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    [SerializeField] HitBox[] hitBoxes;

    private Animator animator;
    private bool comboAsked;

    protected void Start() {
        animator = GetComponentInParent<Animator>();
    }

    public override bool CanAttack() {
        return true;
    }

    public override void Attack() {
        //If already in attack, ask for combo.
        comboAsked = IsAttacking;

        IsAttacking = true;
        animator.SetBool("MeleeAttack", true);
    }

    public void EndAttack(bool forceEnd = false) {
        //En attack if no combo is asked or if attack end is forced
        if (!comboAsked || forceEnd) {
            IsAttacking = false;
            onEndAttack.Invoke();
        }

        //When leaving attack, 
        comboAsked = false;
    }

    public void ToggleHitBoxes(bool state) {
        foreach (HitBox hitBox in hitBoxes) {
            hitBox.gameObject.SetActive(state);
        }
    }


}
