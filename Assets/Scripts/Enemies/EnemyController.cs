﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius=5f;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()    
    {
        agent = GetComponent<NavMeshAgent>();
       PlayerController player = FindObjectOfType<PlayerController>();
        if (player) {
            target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position,transform.position);   
        if (distance<=lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance<=agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f) ;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerManager.instance.bulletLayer)
        {
            Destroy(gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
