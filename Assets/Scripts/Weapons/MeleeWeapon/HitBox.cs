using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour {

    public delegate void HitEvent(EnemyHealth enemy);
    public HitEvent onHit;

    void OnTriggerEnter(Collider collision) {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth) {
            onHit.Invoke(enemyHealth);
        }
    }
}
