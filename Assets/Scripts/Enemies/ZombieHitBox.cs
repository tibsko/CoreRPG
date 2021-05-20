using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    [SerializeField] ZombieAttack enemyAttack;
   
    void OnTriggerEnter(Collider collision) {
        GenericHealth health = collision.gameObject.GetComponent<GenericHealth>();
        if (health) {
            health.TakeDamage(enemyAttack.attackDamages, gameObject);
        }
    }
}
