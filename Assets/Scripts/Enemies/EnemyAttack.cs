using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour {
    public int attackDamages;
    public bool activateHitbox;
    public float attackRadius = 1f;
    public Transform target;

    private ZombieController zombieController;
    private Animator animator;

    // Start is called before the first frame update
    protected void Start() {
        zombieController = GetComponent<ZombieController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        activateHitbox = false;
    }

    // Update is called once per frame
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

    void OnTriggerEnter(Collider collision) {
        PlayerAttack player = collision.GetComponent<PlayerAttack>();
        if (player) {
            transform.LookAt(player.transform.position);
        }
    }
}
