using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    [SerializeField] EnemyAttack enemyAttack;
   
    public void Start() {
        
    }
    void OnTriggerEnter(Collider collision) {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        DoorHealth doorHealth = collision.gameObject.GetComponent<DoorHealth>();
        if (playerHealth) {
            playerHealth.TakeDamage(enemyAttack.attackDamages);
        }
        else if (doorHealth) {
            doorHealth.TakeDamage(enemyAttack.attackDamages);
            Debug.Log("trigger");
        }
    }
}
