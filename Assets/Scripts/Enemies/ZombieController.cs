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
    [SerializeField] float detectionFrequency = 2f;

    private GenericHealth target;
    public GenericHealth Target {
        get {
            return target;
        }
        set {
            if (value != null) {
                if (value.CurrentHealth > 0) {
                    target = value;
                }
                else {
                    Debug.Log($"Can't asign {value.name} as target beccause it has 0 PV");
                }
            }
        }
    }
    public bool HasLeavedSpawn { get; set; }

    private bool overrideTarget = false;
    private NavMeshAgent agent;
    private Animator animator;

    void Start() {
        HasLeavedSpawn = false;
        animator = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {

        //Reapeat update target
        InvokeRepeating(nameof(UpdateTarget), 0, detectionFrequency);

        //If zombie has a target
        if (Target) {
            //Update navmesh agent
            agent.SetDestination(Target.transform.position);
            float distance = Vector3.Distance(Target.transform.position, transform.position);

            //Update rotation
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

        //Update animator
        if (agent.velocity.magnitude > 0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    public void SetMove(bool move) {
        agent.isStopped = !move;

        if (move) {
            if (Target) agent.SetDestination(Target.transform.position);
        }
        else {
            animator.SetFloat("Speed", 0);
        }
    }

    private void FaceTarget() {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void UpdateTarget() {

        //If target is dead or null
        if (!Target || Target.CurrentHealth <= 0) {
            Target = ReferenceManager.instance.GetNearestPlayer(transform.position).GetComponent<GenericHealth>();
        }

        if (Target.CompareTag("Player")) {
            Target = ReferenceManager.instance.GetNearestPlayer(transform.position).GetComponent<GenericHealth>();
        }
    }
}
