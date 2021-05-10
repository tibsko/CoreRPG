using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBullet : Bullet
{
    //Layer
    [SerializeField] LayerMask collisionLayers;

    //Stats
    public float Velocity { get; private set; }

    //Set up
    private Vector3 startPosition;
    private new Rigidbody rigidbody;

    void Start() {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();

        if (!initialized) {
            Destroy(gameObject);
            Debug.LogError($"Bullet ({gameObject.name}) was not initialized !");
        }
    }

    void Update() {
        rigidbody.velocity = transform.forward * Velocity;
        RangeDestroy();
    }

    public override void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range, float _lifeTime) {
        transform.rotation = Quaternion.Euler(rotation);
        Damages = _damages;
        Velocity = _velocity;
        Range = _range;
        initialized = true;
    }

    void RangeDestroy() {
        if (Vector3.Distance(transform.position, startPosition) >= Range) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collisionLayers.ContainsLayer(collision.gameObject.layer)) {

            Vector3 bulletPosition = gameObject.transform.position;
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth) {
                enemyHealth.TakeDamage(Damages, gameObject);
            }

            //Impact particules
            ImpactParticlesEmitter emitter = collision.gameObject.GetComponent<ImpactParticlesEmitter>();
            if (emitter) {
                emitter.InstantiateParticule(bulletPosition, -transform.forward);
            }
            Destroy(gameObject);

            //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        }
    }
}
