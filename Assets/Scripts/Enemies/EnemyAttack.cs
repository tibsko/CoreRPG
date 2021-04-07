using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRadius = 1f;
    public int attackDamages;

    private Transform target;
    private Animator animator;
    [SerializeField] ZombieHitBox zombieHitBox;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        if (distance<attackRadius) {
            animator.SetBool("isAttacking", true);
            zombieHitBox.ActivateHitBox();
        }
        else {
            animator.SetBool("isAttacking", false);
            zombieHitBox.DesactivateHitBox();

        }

    }

    void OnTriggerEnter(Collider collision) {
        PlayerShooter player = collision.GetComponent<PlayerShooter>();
        if (player) {
            transform.LookAt(player.transform.position);
           
        }
    }
}
