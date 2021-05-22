using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : Bullet {

    [SerializeField] LayerMask damagesLayers;
    [SerializeField] float explosionRadius = 5f;

    private ParticleSystem particleSystem;
    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
        particleSystem = GetComponent<ParticleSystem>();

        if (!initialized) {
            Destroy(gameObject);
            Debug.LogError($"Bullet ({gameObject.name}) was not initialized !");
        }
    }

    void Update() {
        RangeDestroy();
    }

    public override void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range, float _lifeTime) {
        transform.rotation = Quaternion.Euler(rotation);
        Damages = _damages;
        Range = _range;
        initialized = true;
    }

    void RangeDestroy() {
        if (Vector3.Distance(transform.position, startPosition) >= Range) {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other) {

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        Collider[] cols = Physics.OverlapSphere(collisionEvents[0].intersection, explosionRadius, damagesLayers);
        foreach (Collider col in cols) {
            if (col.GetComponent(out GenericHealth health)) {
                health.TakeDamage(Damages, this.gameObject);
            }
            //GenericHealth health = col.GetComponent<GenericHealth>();
            //if (health) {
            //    health.TakeDamage(Damages, this.gameObject);
            //}
        }
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
