using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    [SerializeField] float doorDetectionRadius = 3f;

    public bool IsInRoom { get; set; }
    public Transform Target { get; set; }

    private NavMeshAgent agent;
    private Animator animator;
    private FenceHealth doorHealth;

    // Start is called before the first frame update
    void Start() {
        IsInRoom = false;
        animator = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        Targeting();
        if (Target) {
            agent.SetDestination(Target.position);
            float distance = Vector3.Distance(Target.position, gameObject.transform.position);
            if (distance <= agent.stoppingDistance) {
                FaceTarget();
            }
        }
        else {
            Debug.Log($"Zombie {gameObject.name} doesn't have target");
        }

        if (agent.velocity.magnitude > 0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    void FaceTarget() {
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Targeting() {
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
                Collider[] cols = Physics.OverlapSphere(transform.position, doorDetectionRadius, ReferenceManager.instance.doorLayer);
                if (cols.Length > 0) {
                    Collider closest = transform.position.GetClosest(cols);
                    doorHealth = closest.GetComponent<FenceHealth>();
                }
            }
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, doorDetectionRadius);
    }
}
