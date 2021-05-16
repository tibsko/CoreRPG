using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    private GenericHealth connectedFence;

    private void Start() {
        connectedFence = GetComponentInParent<GenericHealth>();
    }

    public void OnTriggerStay(Collider other) {

        ZombieController zombieController = other.GetComponent<ZombieController>();
        if (zombieController && !zombieController.HasLeavedSpawn) {
            zombieController.Target = connectedFence;
        }
    }

    public void OnTriggerExit(Collider other) {

        ZombieController zombieController = other.GetComponent<ZombieController>();
        if (zombieController && !zombieController.HasLeavedSpawn) {
            zombieController.HasLeavedSpawn = true;
            zombieController.Target = null;
        }
    }
}
