using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowerUtilitary : MonoBehaviour {
    [SerializeField] LayerMask mask;
    [SerializeField] float speedRateDown;
    
    void OnTriggerStay(Collider other) {
        if (mask.ContainsLayer(other.gameObject.layer)) {
            if (other.HasComponent(out NavMeshAgent zombie)) {
                if (other.HasComponent(out ZombieController controller)&& !controller.IsSlowed) {
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
