using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JDBullet : MonoBehaviour {
    public float velocity;
    private Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        rigidbody.velocity = transform.forward * velocity;
    }

    public void Init(Vector3 rotation) {
        //transform.LookAt(transform.position + direction.normalized * 10);
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void OnTriggerEnter(Collider collision) {
        Vector3 bulletPosition = gameObject.transform.position;
        ParticuleEmitter emitter = collision.gameObject.GetComponent<ParticuleEmitter>();
        if (emitter) {
            emitter.InstantiateParticule(bulletPosition);
        }
        if (collision.gameObject.layer != gameObject.layer) {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth) {
                enemyHealth.TakeDamage(10, gameObject);
                Destroy(gameObject); //trouver solution pour colision
            }
        }
    }
}
