using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerAttack : EnemyAttack {
    public bool isScreaming;

    private bool hasExplose;
    private Animator animator;

    [SerializeField] float exploseDistance;
    [SerializeField] float exploseRadius;
    [SerializeField] float delayExplose;
    [SerializeField] int exploseDamages;
    [SerializeField] GameObject exploseParticule;

    // Start is called before the first frame update
    void Start() {

        activatehitbox = false;
        animator = gameObject.GetComponentInChildren<Animator>();

        isScreaming = false;
        hasExplose = false;
    }

    // Update is called once per frame
    void Update() {
        if (hasExplose) {
            return;
        }
        if (target != null) {
            float distance = Vector3.Distance(target.position, gameObject.transform.position);
            DoorHealth door = target.GetComponent<DoorHealth>();
            Debug.Log(target.name);
            if (door && door.currentHealth > 0) {
                if (distance < attackRadius) {
                    animator.SetBool("isAttacking", true);
                }
                else {
                    animator.SetBool("isAttacking", false);
                }
            }
            else if (target == PlayerManager.instance.player.transform) {
                animator.SetBool("isAttacking", false);
                if (distance <= exploseDistance) {
                    animator.SetBool("isScreaming", true);
                    isScreaming = true;
                }
                else {
                    animator.SetBool("isScreaming", false);
                }
            }
        }
        if (isScreaming && !hasExplose) {
            hasExplose = true;
            Invoke(nameof(BoomerExplose), delayExplose);
        }
    }

    void OnTriggerEnter(Collider collision) {
        PlayerShooter player = collision.GetComponent<PlayerShooter>();
        if (player) {
            transform.LookAt(player.transform.position);
        }
    }

    public void BoomerExplose() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, exploseRadius, LayerManager.instance.playerLayer);
        if (colliders.Length > 0) {
            foreach (Collider col in colliders) {
                PlayerHealth playerhealth = col.GetComponent<PlayerHealth>();
                if (playerhealth) {
                    playerhealth.TakeDamage(exploseDamages, gameObject);
                }
                break;
            }
        }
        GameObject particule = Instantiate(exploseParticule, transform.position, Quaternion.identity);
        Destroy(particule, 1.5f);
        Destroy(gameObject, .2f);
        this.enabled = false;
    }
}
