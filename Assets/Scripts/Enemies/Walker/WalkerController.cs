﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkerController : MonoBehaviour {
    public bool isInRoom;

    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private WalkerAttack enemyAttack;

    // Start is called before the first frame update
    void Start() {
        isInRoom = false;

        enemyAttack = GetComponent<WalkerAttack>();

        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update() {
        Targeting();
        enemyAttack.target = target;
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
        //DoorHealth door = doorDetector.doorDetected.GetComponent<DoorHealth>();
        //if (isInRoom) {
        //    target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
        //}
        //else if (door && !isInRoom) {
        //    if (door.currentHealth <= 0) {
        //        target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
        //    }
        //    else {
        //        if (doorDetector.doorDetected) {
        //            target = doorDetector.doorDetected;
        //        }
        //        isInRoom = false;
        //    }
        //}
        //else {
        //    target = ReferenceManager.instance.GetNearestPlayer(transform.position).transform;
        //}
    }

}
