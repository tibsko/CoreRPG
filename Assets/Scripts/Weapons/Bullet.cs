using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 currentPosition;
    Vector3 startPos;
    public BulletData bulletData;
    float currentTime;

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
        if (currentTime > bulletData.lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            CharacterHealth playerHealth = collision.gameObject.GetComponent<CharacterHealth>();
            playerHealth.TakeDamage(bulletData.damage);
        }

            Destroy(gameObject); //trouver solution pour colision
    }

    void DestroyRange()
    {
        if (Vector3.Distance(currentPosition, startPos) >= bulletData.maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bulletData.radius);
    }
}
