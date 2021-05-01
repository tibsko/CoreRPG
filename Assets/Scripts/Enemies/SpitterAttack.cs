using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAttack : EnemyAttack {

    private Animator animator;

    public float attackDistance;
    public bool isSpitting=false;

    // Start is called before the first frame update
    protected void Start() {
        activatehitbox = false;
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            float distance = Vector3.Distance(target.position, gameObject.transform.position);
            DoorHealth door = target.GetComponent<DoorHealth>();
            if (door && door.currentHealth > 0) {
                if (distance < attackRadius) {
                    Debug.Log("wall Attack");
                    animator.SetBool("isAttacking", true);
                }
                else {
                    animator.SetBool("isAttacking", false);
                }
            }
            else if (target == PlayerManager.instance.player.transform) {
                animator.SetBool("isAttacking", false);
                if (distance <= attackDistance) {
                    animator.SetBool("isScreaming", true);
                    isSpitting = true;
                    SpitAttack();
                }
                else {
                    animator.SetBool("isScreaming", false);
                    isSpitting = false;
                }
            }
        }
    }
    private void SpitAttack() {
        Debug.Log("Spit");
    }
}
