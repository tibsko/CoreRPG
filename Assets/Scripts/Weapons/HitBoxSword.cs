using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSword : MonoBehaviour
{
    private SphereCollider hitBox;
    [SerializeField] Sword sword;
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) {
        Debug.Log(collision.gameObject.name);
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth) {
            Debug.Log("hit");

            enemyHealth.TakeDamage(sword.damages);
        }
    }

}
