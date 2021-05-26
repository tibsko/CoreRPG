using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leurre : MonoBehaviour
{
    [SerializeField] LayerMask mask;

    private GenericHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<GenericHealth>();   
    }
    void OnTriggerStay(Collider other) {
        if (mask.ContainsLayer(other.gameObject.layer)) {
            if (other.HasComponent(out NavMeshAgent zombie)) {
                if (other.HasComponent(out ZombieController controller)) {
                    controller.Target = health;
                    Debug2.Log("inside bro");
                }
            }
        }
    }
}
