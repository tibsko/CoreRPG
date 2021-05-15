using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAttack : EnemyAttack {

    private Animator animator;

    [SerializeField] GameObject dieParticule;
    [SerializeField] float dieRadius;
    [SerializeField] float poisonTime;

    public float attackDistance;
    public bool isSpitting=false;

    // Start is called before the first frame update
    protected void Start() {
        activateHitbox = false;
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            float distance = Vector3.Distance(target.position, gameObject.transform.position);
            FenceHealth door = target.GetComponent<FenceHealth>();
            if (door && door.CurrentHealth > 0) {
                if (distance < attackRadius) {
                    animator.SetBool("isAttacking", true);
                }
                else {
                    animator.SetBool("isAttacking", false);
                }
            }
            else if (target == ReferenceManager.instance.player.transform) {
                animator.SetBool("isAttacking", false);
                if (distance <= attackDistance) {
                    animator.SetBool("isScreaming", true);
                    isSpitting = true;
                }
                else {
                    animator.SetBool("isScreaming", false);
                    isSpitting = false;
                }
            }
        }
    }
    public void DieBehaviour() {
        GameObject poisonEffect = Instantiate(dieParticule, transform.position, Quaternion.identity);
        poisonEffect.transform.localScale = new Vector3(dieRadius, dieRadius, dieRadius);
        Destroy(poisonEffect,poisonTime);
        Destroy(gameObject);
    }
}
