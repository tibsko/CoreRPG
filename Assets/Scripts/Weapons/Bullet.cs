using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 5f;
    float timer = 0f;
    public float radius;
    [SerializeField] float explosionForce = 10f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                if (collider.attachedRigidbody != null)
                    collider.attachedRigidbody.AddForce((collider.transform.position - transform.position).normalized * explosionForce, ForceMode.Impulse);
                
            }
        }
        Destroy(gameObject); //trouver solution pour colision
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
