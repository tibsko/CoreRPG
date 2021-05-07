using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammeBullet : Bullet {

    public float damagesRate;

    public float startHeight = 2.2f;
    public float endHeight = 8f;

    private float stepHeight;
    private float delay = 1.5f;
    private float countDown;
    private float stepDelay;
    private Timer lifeTimer;
    private float lifeTime = 1.8f;

    private CapsuleCollider capsule;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start() {
        lifeTimer = new Timer(lifeTime);
        particles = GetComponent<ParticleSystem>();
        capsule = GetComponent<CapsuleCollider>();
        stepDelay = delay / (endHeight - startHeight);
        countDown = stepDelay;
        Debug.Log(stepDelay);

        stepHeight = (endHeight - startHeight) * (stepDelay);
        Debug.Log(stepHeight);
        capsule.height = startHeight;

        capsule.center = new Vector3(transform.position.x, transform.position.y, capsule.height * 0.5f);

        if (!initialized) {
            Destroy(gameObject);
            Debug.LogError($"Bullet ({gameObject.name}) was not initialized !");
        }
    }

    // Update is called once per frame
    void Update() {

        if (capsule.height <= endHeight) {
            countDown -= Time.deltaTime;
            if (countDown <= 0) {
                capsule.height += stepHeight;
                countDown = stepDelay;
            }
            capsule.center = new Vector3(0, 0, capsule.height * 0.5f);
        }
        //else {
        //    capsule.height = endHeight; 
        //}

        lifeTimer.Tick(Time.deltaTime);
        if (lifeTimer.Done) {
            particles.Stop();
            Destroy(gameObject, 2f);
        }
    }

    void OnTriggerStay(Collider collider) {
        EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth) {
            StartCoroutine(DamageTime(enemyHealth));
        }
    }

    IEnumerator DamageTime(EnemyHealth enemy) {
        yield return new WaitForSeconds(damagesRate);
        enemy.TakeDamage(Damages, gameObject);
    }

    public override void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range) {
        Damages = _damages;
        initialized = true;
        lifeTime = _velocity;
    }
}
