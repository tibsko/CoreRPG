using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius=10f;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()    
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        if (distance<=agent.stoppingDistance) {
            FaceTarget();
        }
        if (agent.velocity.magnitude>0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
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
}
