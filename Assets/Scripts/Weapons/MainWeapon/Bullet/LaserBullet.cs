using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet {

    private ParticleSystem particleSystem;
    private float lifeTime;

    void Start() {
        particleSystem = GetComponent<ParticleSystem>();

        Invoke(nameof(Stop), lifeTime);

        if (!initialized) {
            Destroy(gameObject);
            Debug.LogError($"Bullet ({gameObject.name}) was not initialized !");
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void Stop() {
        particleSystem.Stop();
        Destroy(gameObject, 1f);
    }

    private void OnParticleCollision(GameObject other) {

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health) {
            health.TakeDamage(Damages, this.gameObject);
        }
    }

    public override void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range, float _lifeTime) {
        transform.rotation = Quaternion.Euler(rotation);
        Damages = _damages;
        lifeTime = _lifeTime;
        initialized = true;
    }

}
