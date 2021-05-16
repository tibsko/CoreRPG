using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float destroyTimer;

    public void EnableRagdoll() {
        gameObject.layer = 0;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<ZombieAttack>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        SetKinematic(false);
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, destroyTimer);
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
