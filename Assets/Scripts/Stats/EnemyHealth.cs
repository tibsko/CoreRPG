using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : GenericHealth {

    [SerializeField] float timer;

    public void Die() {
        gameObject.layer = 0;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        SetKinematic(false);
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject,timer);
        this.enabled = false;
    }

    private void SetKinematic(bool newValue) {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = newValue;
        }
    }
}
