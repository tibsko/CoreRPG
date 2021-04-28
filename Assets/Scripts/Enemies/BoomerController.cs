using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoomerController : MonoBehaviour {
    public bool isInside;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private BoomerAttack boomerAttack;
    private DoorDetector doorDetector;

    [SerializeField] float speedRunDistance = 7f;
    [SerializeField] float speedRun = 5f;
    [SerializeField] float speedWalk = 2f;


    // Start is called before the first frame update
    void Start() {
        doorDetector = GetComponentInChildren<DoorDetector>();
        isInside = false;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedWalk;

        boomerAttack = GetComponent<BoomerAttack>();

        animator = gameObject.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update() {
        Targeting();
        boomerAttack.target = target;
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        agent.SetDestination(target.position);
        if (boomerAttack.isScreaming == false) {
            if (distance <= agent.stoppingDistance) {
                FaceTarget();
            }
            if (agent.velocity.magnitude > 0.01f) {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
            if (target == PlayerManager.instance.player.transform) {
                if (distance <= speedRunDistance) {
                    agent.speed = speedRun;
                }
                else {
                    agent.speed = speedWalk;
                }
            }
        }
        if (boomerAttack.isScreaming == true) {
            animator.SetFloat("Speed", 0);
            agent.speed = 0f;
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
            agent.stoppingDistance = 2f;
        }
        else if (door && !isInside) {
            if (door.currentHealth <= 0) {
                target = PlayerManager.instance.player.transform;
                isInside = true;
                agent.stoppingDistance = 2f;
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
