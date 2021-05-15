using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpitterController : MonoBehaviour
{
    private bool isInside;
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private SpitterAttack enemyAttack;
    private DoorDetector doorDetector;
    private float speed = 2f;

    // Start is called before the first frame update
    void Start() {
        isInside = false;
        doorDetector = GetComponentInChildren<DoorDetector>();

        enemyAttack = GetComponent<SpitterAttack>();

        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update() {
        Targeting();
        enemyAttack.target = target;

        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        if (distance <= agent.stoppingDistance) {
            FaceTarget();
        }

        if (agent.velocity.magnitude > 0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if (enemyAttack.isSpitting == true) {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            FaceTarget();
            animator.SetFloat("Speed", 0);

        }
        else {
            agent.speed = speed;
            agent.SetDestination(target.position);
        }

    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Targeting() {
        DoorHealth door = doorDetector.doorDetected.GetComponent<DoorHealth>();
        if (isInside) {
            target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
        }
        else if (door && !isInside) {
            if (door.currentHealth <= 0) {
                target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
            }
            else {
                if (doorDetector.doorDetected) {
                    target = doorDetector.doorDetected;
                }
                isInside = false;
            }
        }
        else {
            target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
        }
    }

}
