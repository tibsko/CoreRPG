using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSword : MonoBehaviour
{
    [SerializeField] Sword sword;

    void OnTriggerEnter(Collider collision) {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth) {

            enemyHealth.TakeDamage(sword.Damages,gameObject);
        }
    }


}
