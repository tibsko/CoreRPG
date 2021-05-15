using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    [SerializeField] ZombieAttack enemyAttack;
   
    void OnTriggerEnter(Collider collision) {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        FenceHealth doorHealth = collision.gameObject.GetComponent<FenceHealth>();
        if (playerHealth) {
            playerHealth.TakeDamage(enemyAttack.attackDamages, gameObject);
        }
        else if (doorHealth) {
            doorHealth.TakeDamage(enemyAttack.attackDamages, gameObject);
            doorHealth.UpdateFenceBoards();
        }
    }
}
