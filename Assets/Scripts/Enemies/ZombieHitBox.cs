using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    [SerializeField] EnemyAttack enemyAttack;
    private SphereCollider hitBox;
    public void Start() {
        hitBox = gameObject.GetComponent<SphereCollider>();
        if (hitBox == null) {
            Debug.LogError("No Hitbox");
        }
    }
    void OnTriggerEnter(Collider collision) {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth) {

            playerHealth.TakeDamage(enemyAttack.attackDamages);
        }
    }
    public void ActivateHitBox() {
        Debug.Log("Activate HitBox");
        hitBox.enabled = true;
    }
    public void DesactivateHitBox() {
        hitBox.enabled = false;
        Debug.Log("DeActivate HitBox");

    }

}
