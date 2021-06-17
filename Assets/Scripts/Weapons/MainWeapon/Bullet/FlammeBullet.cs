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
    private float lifeTime;

    private CapsuleCollider capsule;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start() {
        particles = GetComponent<ParticleSystem>();
        capsule = GetComponent<CapsuleCollider>();
        stepDelay = delay / (endHeight - startHeight);
        countDown = stepDelay;

        stepHeight = (endHeight - startHeight) * (stepDelay);
        capsule.height = startHeight;

        capsule.center = new Vector3(transform.position.x, transform.position.y, capsule.height * 0.5f);

        Invoke(nameof(Stop), lifeTime);

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
    }

    private void Stop() {
        particles.Stop();
        Destroy(gameObject, 1f);
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

    public override void InitializeBullet(Vector3 rotation, float _damages, float _velocity, float _range, float _lifeTime) {
        Damages = _damages;
        initialized = true;
        lifeTime = _lifeTime;
    }
}
