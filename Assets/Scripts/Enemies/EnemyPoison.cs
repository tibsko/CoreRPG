using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoison : MonoBehaviour
{
    private float countDown;

    [SerializeField] int poisonDamage;
    [SerializeField] float damageSpeed;
    // Start is called before the first frame update
    void Start()
    {
        countDown = damageSpeed;
    }

    void OnTriggerStay(Collider collider) {
        PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth) {
            countDown -= Time.deltaTime;
            if (countDown <= 0) {
                playerHealth.TakeDamage(poisonDamage, gameObject);
                countDown = damageSpeed;
            }
        }
    }

 
}
