using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAttack : ZombieAttack {

    protected override void Update() { }
    protected void FixedUpdate() {
        if (Target != null) {
            float distance = Vector3.Distance(Target.transform.position, gameObject.transform.position);
            animator.SetBool("IsAttacking", distance <= attackRadius);
        }
    }

    protected override void DieBehaviour() {
        this.enabled = false;
    }
}
