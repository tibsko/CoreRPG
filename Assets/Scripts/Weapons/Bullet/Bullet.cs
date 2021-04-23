using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    Vector3 currentPosition;
    Vector3 startPos;
    [HideInInspector] public FireWeaponData weaponData;
    float currentTime;
    [HideInInspector] public bool damagePerDistance;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        DestroyRange();
        currentTime = Time.deltaTime;
        if (currentTime > weaponData.lifeTimeBullet)
        {
            Destroy(gameObject);
        }
    }
    
    
    void OnTriggerEnter(Collider collision)
    {
        //Collider[] colliders = Physics.OverlapSphere(transform.position, bulletData.radius);
        //foreach (var collider in colliders)
        //{
        //    if (collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        //    {
        //        if (collider.attachedRigidbody != null)
        //            collider.attachedRigidbody.AddForce((collider.transform.position - transform.position).normalized * bulletData.explosionForce, ForceMode.Impulse);

        //    }
        //}
        if (collision.gameObject.layer != gameObject.layer) {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth) {
                if (!damagePerDistance)
                    enemyHealth.TakeDamage(weaponData.damages,gameObject);
                else {
                    enemyHealth.TakeDamage((int)Mathf.Ceil(Vector3.Distance(currentPosition, startPos) / weaponData.maxDistance * weaponData.damages),gameObject);
                }
                Destroy(gameObject); //trouver solution pour colision
            }

        }



    }

    void DestroyRange()
    {
        if (Vector3.Distance(currentPosition, startPos) >= weaponData.maxDistance)
        {
            Destroy(gameObject);
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, weaponData.radiusBullet);
    //}
}
