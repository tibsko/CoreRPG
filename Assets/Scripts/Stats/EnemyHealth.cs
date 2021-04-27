using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : CharacterHealth {

    [SerializeField] float timer;
    // Update is called once per frame
    public void Die() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        GetComponentInChildren<EnemyHealth>().enabled = false;
        SetKinematic(false);
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject,timer);
    }
    private void SetKinematic(bool newValue) {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = newValue;
        }
    }
}
