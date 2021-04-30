using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpitterController : MonoBehaviour
{
    public bool isInside;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private SpitterAttack enemyAttack;
    private DoorDetector doorDetector;

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
        Debug.Log(target.name);
        agent.SetDestination(target.position);
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        if (distance <= agent.stoppingDistance) {
            FaceTarget();
        }
        if (agent.velocity.magnitude > 0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
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
            target = PlayerManager.instance.player.transform;
        }
        else if (door && !isInside) {
            if (door.currentHealth <= 0) {
                target = PlayerManager.instance.player.transform;
                isInside = true;
            }
            else {
                if (doorDetector.doorDetected) {
                    target = doorDetector.doorDetected;
                }
                isInside = false;
            }
        }
        else {
            target = PlayerManager.instance.player.transform;
        }
    }

}
