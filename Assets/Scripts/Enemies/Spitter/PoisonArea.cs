using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour {

    [SerializeField] int damages = 5;
    [SerializeField] float damageFrequency = 0.5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float radius = 2f;

    private ParticleSystem particles;

    void Start() {
        InvokeRepeating(nameof(DamageBurst), 0, damageFrequency);
        Invoke(nameof(Stop), lifeTime);
    }

    private void DamageBurst() {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, ReferenceManager.instance.playerLayer);
        foreach (Collider collider in cols) {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth) {
                playerHealth.TakeDamage(damages, gameObject);
            }
        }
    }

    private void Stop() {
        particles.Stop();
        this.enabled = false;
        Destroy(gameObject,1);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
