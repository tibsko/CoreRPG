using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAttack : ZombieAttack {

    [SerializeField] float spitDistance = 5f;
    [SerializeField] float poisonRadius = 2f;
    [SerializeField] float poisonDuration = 5f;
    [SerializeField] GameObject dieParticule;

    protected override void Update() {
        if (Target != null) {
            float distance = Vector3.Distance(Target.transform.position, gameObject.transform.position);

            if (Target.CompareTag("Player")) {
                animator.SetBool("IsAttacking", false);
                bool attack = distance <= spitDistance;
                animator.SetBool("IsScreaming", attack);
                zombieController.SetMove(!attack);
            }
            else if(Target.CompareTag("Fence")) {
                bool attack = distance <= attackRadius;
                zombieController.SetMove(!attack);
                animator.SetBool("IsAttacking", attack);
            }
        }
    }

    protected override void DieBehaviour() {
        GameObject poisonEffect = Instantiate(dieParticule, transform.position, Quaternion.identity);
        poisonEffect.transform.localScale = new Vector3(poisonRadius, poisonRadius, poisonRadius);
        Destroy(gameObject);
        Destroy(poisonEffect, poisonDuration);
    }
}
