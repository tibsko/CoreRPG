using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoison : MonoBehaviour
{
    [SerializeField] int poisonDamage;
    [SerializeField] float damageSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider collider) {
        PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth) {
            StartCoroutine(DamageTime(playerHealth));
        }
    }

    IEnumerator DamageTime(PlayerHealth player) {
        yield return new WaitForSeconds(damageSpeed);
        player.TakeDamage(poisonDamage, gameObject);
    }

}
