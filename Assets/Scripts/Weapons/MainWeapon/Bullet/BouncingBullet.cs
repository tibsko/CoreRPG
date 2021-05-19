using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullet : Bullet {
    [SerializeField] float bouncingRadius;
    [SerializeField] int bouncingNb;
    [SerializeField] float distanceInstantiat;
    [SerializeField] GameObject effect;
    [SerializeField] float lifeTime;

    private bool firstEnemy = true;

    public float Velocity { get; private set; }

    private Vector3 nextEnemyPosition;
    private Vector3 newInitialPosition;
    private new Rigidbody rigidbody;

    private List<GameObject> enemies = new List<GameObject>();
    public float delay = .5f;
    private float timerContact = 0;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        timerContact = 0;
        Destroy(gameObject, lifeTime);
    }
    void Update() {
        if (firstEnemy) {
            rigidbody.velocity = transform.forward * Velocity;
        }
        else {
            rigidbody.velocity = new Vector3(nextEnemyPosition.x - newInitialPosition.x, 0, nextEnemyPosition.z - newInitialPosition.z).normalized * Velocity;
        }
        if (timerContact > 0.0)
            timerContact -= Time.deltaTime;



    }

    void OnTriggerEnter(Collider other) {
        if (ReferenceManager.instance.enemyLayer.ContainsLayer(other.gameObject.layer)) {
            Vector3 bulletPosition = gameObject.transform.position;
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth) {
                enemyHealth.TakeDamage(Damages, gameObject);
                if (timerContact <= 0.0) {
                    timerContact = delay;
                    bouncingNb -= 1;
                }
                firstEnemy = false;
                enemies.Add(enemyHealth.gameObject);
                newInitialPosition = transform.position;
                if (bouncingNb < 1) {
                    Destroy(gameObject);
                }
                else {
                    FindNearestEnemy();
                }
            }

            //Impact particules
            ImpactParticlesEmitter emitter = other.gameObject.GetComponent<ImpactParticlesEmitter>();
            if (emitter) {
                emitter.InstantiateParticule(bulletPosition, -transform.forward);
            }
            else if (effect != null) {
                GameObject particule = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(particule, 2f);
            }
        }
    }

    private void FindNearestEnemy() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bouncingRadius, ReferenceManager.instance.enemyLayer);
        if (colliders.Length > 0) {
            float distanceMin = 100f;
            GameObject enemyGo = null;
            foreach (Collider col in colliders) {
                if (!enemies.Contains(col.gameObject) && col.gameObject.GetComponent<GenericHealth>()) {
                    float distance = Vector3.Distance(transform.position, col.gameObject.transform.position);
                    if (distance < distanceMin) {
                        distanceMin = distance;
                        nextEnemyPosition = col.transform.position;
                        enemyGo = col.gameObject;
                    }
                }
            }
            if (!enemyGo) {
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    public override void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range, float _lifeTime) {
        transform.rotation = Quaternion.Euler(rotation);
        Damages = _damages;
        Velocity = _velocity;
        Range = _range;
        lifeTime = _lifeTime;
        initialized = true;
    }

}


