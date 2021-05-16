using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour {

    [SerializeField] float destroyTimer;

    private Rigidbody mainBody;
    private List<Rigidbody> ragdollBodies;

    private void Start() {
        mainBody = GetComponent<Rigidbody>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>().ToList();
        ragdollBodies.Remove(mainBody);
    }

    public void EnableRagdoll() {
        //gameObject.layer = 0;
        SetLayerRecursively(gameObject, LayerMask.NameToLayer("DeadBody"));

        //Disable zombie behaviour
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<ZombieAttack>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;

        //Activate ragdoll rigidbodies
        mainBody.velocity = Vector3.zero;
        foreach (Rigidbody rb in ragdollBodies) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }

        //Call destruction
        Destroy(gameObject, destroyTimer);
    }

    void SetLayerRecursively(GameObject obj, int newLayer) {
        if (null == obj) {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform) {
            if (null == child) {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
