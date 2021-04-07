using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRadius = 1f;
    public int attackDamages;
    public bool activatehitbox;
    private Transform target;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        activatehitbox = false;
        target = PlayerManager.instance.player.transform;
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        if (distance<attackRadius) {
            animator.SetBool("isAttacking", true);
        }
        else {

            animator.SetBool("isAttacking", false);
        }

    }

    void OnTriggerEnter(Collider collision) {
        PlayerShooter player = collision.GetComponent<PlayerShooter>();
        if (player) {
            transform.LookAt(player.transform.position);
           
        }
    }
}
