using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowerUtilitary : MonoBehaviour {
    [SerializeField] LayerMask mask;
    [SerializeField] float speedRateDown;
    // Start is called before the first frame update
    void Start() {

    }


    void OnTriggerEnter(Collider other) {
        if (mask.ContainsLayer(other.gameObject.layer)) {
            if (other.HasComponent(out NavMeshAgent zombie)) {
                if (other.HasComponent(out ZombieController controller)) {
                    controller.IsSlowed = true;
                    zombie.speed = zombie.speed / speedRateDown;
                }
            }
        }
    }
    void OnTriggerExit(Collider other) {
        if (mask.ContainsLayer(other.gameObject.layer)) {
            if (other.HasComponent(out NavMeshAgent zombie)) {
                other.GetComponent<ZombieController>().IsSlowed = false;

            }
        }
    }
}
