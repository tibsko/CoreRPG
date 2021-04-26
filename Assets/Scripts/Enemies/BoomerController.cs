using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoomerController : MonoBehaviour
{
    public float lookRadius = 10f;
    public bool isInside;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private BoomerAttack boomerAttack;

    private float doorDetectorRadius = 3f;
    [SerializeField] float speedRunDistance = 7f;

    // Start is called before the first frame update
    void Start() {
        boomerAttack = GetComponent<BoomerAttack>();
        agent = GetComponent<NavMeshAgent>();
        Tartgeting();
        animator = gameObject.GetComponentInChildren<Animator>();
        boomerAttack.target = target;
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(target.position, gameObject.transform.position);
        agent.SetDestination(target.position);
        if (distance <= agent.stoppingDistance) {
            FaceTarget();
        }
        if (agent.velocity.magnitude > 0.01f) {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        Tartgeting();
        boomerAttack.target = target;

        if (!isInside && target.gameObject.layer == LayerManager.instance.doorLayer) {
            DoorTarget();
        }
        else if(target.gameObject.layer == LayerManager.instance.playerLayer) {
            if (distance <= agent.stoppingDistance) {
                FaceTarget();
                BoomerExplose();
            }
            else if (distance<= speedRunDistance && distance> agent.stoppingDistance) {
                //Speed to player
            }
            else {

            }
        }
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void Tartgeting() {
        if (isInside) {
            target = PlayerManager.instance.player.transform;
        }
        else {
            Collider[] colliders = Physics.OverlapSphere(transform.position, doorDetectorRadius, LayerManager.instance.interactableLayer);
            if (colliders.Length > 0) {
                DoorHealth door = null;
                for (int i = 0; i < colliders.Length; i++) {
                    door = colliders[i].GetComponent<DoorHealth>();
                    if (door) {

                        if (door.currentHealth <= 0) {
                            target = PlayerManager.instance.player.transform;
                        }
                        else {
                            target = colliders[i].transform;
                            isInside = false;
                        }
                        break;
                    }
                }
            }
            else {
                target = PlayerManager.instance.player.transform;
            }
        }

    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerManager.instance.bulletLayer) {
            Destroy(gameObject);
        }
    }
}
