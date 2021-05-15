using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {

    public int attackDamages;
    public bool activateHitbox;
    public float attackRadius = 1f;
    public Transform target { get => zombieController.Target; }
    private Animator animator;
    private ZombieController zombieController;

    protected void Start() {
        zombieController = GetComponent<ZombieController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        activateHitbox = false;
    }

    void Update() {
        if (target != null) {
            float distance = Vector3.Distance(target.position, gameObject.transform.position);
            if (distance < attackRadius) {
                animator.SetBool("IsAttacking", true);

            }
            else {
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    public virtual void DieBehaviour() {

    }
}
