using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : EnemyAttack {
    

    private Animator animator;

    // Start is called before the first frame update
    protected void Start() {
        activatehitbox = false;
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            float distance = Vector3.Distance(target.position, gameObject.transform.position);
            if (distance < attackRadius) {
                animator.SetBool("isAttacking", true);

            }
            else {
                animator.SetBool("isAttacking", false);
            }
        }
    }



    void OnTriggerEnter(Collider collision) {
        PlayerShooter player = collision.GetComponent<PlayerShooter>();
        if (player) {
            transform.LookAt(player.transform.position);
        }
    }
}
