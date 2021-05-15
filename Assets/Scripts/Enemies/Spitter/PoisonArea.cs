using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    [SerializeField] int poisonDamage;
    [SerializeField] float damageRate;
    [SerializeField] float lifeTime;

    private float countDown;

    void Start()
    {
        countDown = damageRate;
    }

    void OnTriggerStay(Collider collider) {
        PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth) {
            countDown -= Time.deltaTime;
            if (countDown <= 0) {
                playerHealth.TakeDamage(poisonDamage, gameObject);
                countDown = damageRate;
            }
        }
    }

 
}
