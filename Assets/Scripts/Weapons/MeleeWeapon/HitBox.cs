using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public int damages;

    void OnTriggerEnter(Collider collision) {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth) {

            enemyHealth.TakeDamage(damages, gameObject);
        }
    }
}
