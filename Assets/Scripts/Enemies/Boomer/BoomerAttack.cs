using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerAttack : ZombieAttack {

    [SerializeField] float explosionDistance = 1f;
    [SerializeField] float explosionRadius = 3f;
    [SerializeField] float explosionDelay = 0.2f;
    [SerializeField] int explosionDamages = 50;
    [SerializeField] GameObject explosionParticles;

    private bool isScreaming = false;
    private bool hasExplosed = false;

    protected override void Update() {
        if (hasExplosed) {
            return;
        }
        if (Target != null) {
            float distance = Vector3.Distance(Target.position, gameObject.transform.position);

            if (Target.CompareTag("Player")) {

                animator.SetBool("IsAttacking", false);
                if (distance <= explosionDistance) {
                    animator.SetBool("IsScreaming", true);
                    Invoke(nameof(BoomerExplose), explosionDelay);
                    zombieController.Move(false);
                    isScreaming = true;
                    hasExplosed = true;
                }
            }
            else if(Target.CompareTag("Fence")) {
                if (distance <= attackRadius) {
                    animator.SetBool("IsAttacking", true);
                }
                else {
                    animator.SetBool("IsAttacking", false);
                }
            }
        }
    }

    protected override void DieBehaviour() {
        BoomerExplose();
        this.enabled = false;
    }

    private void BoomerExplose() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, ReferenceManager.instance.playerLayer);
        if (colliders.Length > 0) {
            foreach (Collider col in colliders) {
                PlayerHealth playerhealth = col.GetComponent<PlayerHealth>();
                if (playerhealth) {
                    Debug.Log("Player hitted");
                    playerhealth.TakeDamage(explosionDamages, gameObject);
                }
                break;
            }
        }
        GameObject particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(particles, 1.5f);
        Destroy(gameObject, .2f);
    }
}
