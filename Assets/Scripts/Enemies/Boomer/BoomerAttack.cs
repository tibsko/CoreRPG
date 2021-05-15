﻿using System.Collections;
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

        activateHitbox = false;
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
            FenceHealth door = target.GetComponent<FenceHealth>();

            if (door && door.CurrentHealth > 0) {
                if (distance < attackRadius) {
                    animator.SetBool("isAttacking", true);
                }
                else {
                    animator.SetBool("isAttacking", false);
                }
            }
            else if (target == ReferenceManager.instance.GetNearestPlayer(transform.position).transform) {
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
        PlayerAttack player = collision.GetComponent<PlayerAttack>();
        if (player) {
            transform.LookAt(player.transform.position);
        }
    }

    public void BoomerExplose() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, exploseRadius, ReferenceManager.instance.playerLayer);
        if (colliders.Length > 0) {
            foreach (Collider col in colliders) {
                PlayerHealth playerhealth = col.GetComponentInParent<PlayerHealth>();
                if (playerhealth) {
                    Debug.Log("Player hitted");
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
