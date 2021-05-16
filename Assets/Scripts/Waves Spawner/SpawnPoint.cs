using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] GenericHealth connectedFence;

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
