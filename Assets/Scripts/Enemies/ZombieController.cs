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
    [SerializeField] float detectionFrequency = 0.1f;

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
                    Debug2.Log($"Can't asign {value.name} as target because it has 0 PV");
                }
            }
            else
                target = value;
        }
    }
    public bool HasLeavedSpawn { get; set; }
    public bool IsStunned { get; set; }

    private NavMeshAgent agent;
    private Animator animator;

    

    void Start() {
        HasLeavedSpawn = false;
        animator = gameObject.GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //Repeat update target
        InvokeRepeating(nameof(UpdateTarget), 0, detectionFrequency);
    }

    void Update() {

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
            if (!agent.isStopped) {
                if (Target.CompareTag("Player") && distance <= speedRunDistance) {
                    agent.speed = runSpeed;
                }
                else {
                    agent.speed = walkSpeed;
                }
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
            Debug2.Log("Enemy stopped", "TDEnemyTouch");
            animator.SetFloat("Speed", 0);
        }
    }
    public void SetStun(bool stun, float duration) {
        IsStunned = stun;
        SetMove(!stun);
        if(stun)
            StartCoroutine(StopController(duration));
    }
    IEnumerator StopController(float duration) {
        yield return new WaitForSeconds(duration);
        IsStunned = false;
        SetMove(true);
    }

    private void FaceTarget() {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void UpdateTarget() {
        //If target is dead or null
        if (!Target || Target.CurrentHealth <= 0 || Target.CompareTag("Player")) {
            Target = ReferenceManager.instance.GetNearestPlayer(transform.position).GetComponent<GenericHealth>();
        }
    }

}

