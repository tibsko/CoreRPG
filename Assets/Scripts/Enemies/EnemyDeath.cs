using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour {

    [SerializeField] float destroyTimer;
    [SerializeField] GameObject mainBody;
    [SerializeField] GameObject ragdollBody;

   

    public void EnableRagdoll() {
        gameObject.layer = LayerMask.NameToLayer("DeadBody");

        //Disable zombie behaviour
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<ZombieAttack>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;

        //Activate ragdoll rigidbodies
        CopyTransformRecursively(mainBody.transform, ragdollBody.transform);
        mainBody.SetActive(false);
        ragdollBody.SetActive(true);

        //Call destruction
        Destroy(gameObject, destroyTimer);
    }

    public void CopyTransformRecursively(Transform source, Transform destination) {
        foreach (Transform sourceChild in source.transform) {
            Transform destinationChild = destination.Find(sourceChild.name);
            if (destinationChild) {
                destinationChild.position = sourceChild.position;
                destinationChild.rotation = sourceChild.rotation;
                destinationChild.localScale = sourceChild.localScale;

                CopyTransformRecursively(sourceChild, destinationChild);
            }
        }
    }

    ///////////////////////////////////////DEBUG//////////////////////////////////
    //[Range(0, 100)]
    //[SerializeField] float bodyPartMass = 10f;
    //[SerializeField] bool applyMass = false;

    //private void OnDrawGizmos() {
    //    if (applyMass) {
    //        var main = GetComponent<Rigidbody>();
    //        var bodies = GetComponentsInChildren<Rigidbody>(true).ToList();
    //        bodies.Remove(main);

    //        foreach (var body in bodies) {
    //            body.mass = bodyPartMass;
    //        }

    //        applyMass = false;
    //    }
    //}
}
