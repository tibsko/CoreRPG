using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    [Header("Speed")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float speedRunDistance = 7f;

    [Header("Detection")]
    [SerializeField] float fenceDetectionRadius = 10f;

    public bool IsInRoom { get; set; }
    public Transform Target { get; set; }

    private NavMeshAgent agent;
    private Animator animator;
    private FenceHealth doorHealth;

    void Start() {
        IsInRoom = false;
        animator = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        Targeting();
        //If zombie has a target
        if (Target) {
            agent.SetDestination(Target.position);
            float distance = Vector3.Distance(Target.position, transform.position);

            if (distance <= agent.stoppingDistance) {
                FaceTarget();
            }

            //Update speed
            if (Target.CompareTag("Player") && distance <= speedRunDistance) {
                agent.speed = runSpeed;
            }
            else {
                agent.speed = walkSpeed;
            }

        }
        else {
            Debug.Log($"Zombie {gameObject.name} doesn't have target");
        }

        if (agent.velocity.magnitude > 0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private void OnEnable() {
        agent.SetDestination(Target.position);   
        agent.isStopped = false;
    }

    private void OnDisable() {
        animator.SetFloat("Speed", 0);
        agent.isStopped = true;
    }

    private void FaceTarget() {
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Targeting() {
        if (IsInRoom) {
            Target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
        }
        else {
            if (doorHealth) {
                if (doorHealth.CurrentHealth <= 0) {
                    Target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
                }
                else {
                    Target = doorHealth.transform;
                }
            }
            else {
                Collider[] cols = Physics.OverlapSphere(transform.position, fenceDetectionRadius, ReferenceManager.instance.doorLayer);
                if (cols.Length > 0) {
                    Collider closest = transform.position.GetClosest(cols);
                    doorHealth = closest.GetComponent<FenceHealth>();
                }
            }
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fenceDetectionRadius);
    }
}
