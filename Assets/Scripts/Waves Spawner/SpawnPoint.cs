using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] GenericHealth connectedFence;

    public void OnTriggerStay(Collider other) {

        ZombieController zombieController = other.GetComponent<ZombieController>();
        if (zombieController) {
            if (zombieController.IsInSpawn) {
                FenceHealth fenceHealth = connectedFence.GetComponent<FenceHealth>();
                if (fenceHealth.CurrentHealth > 0)
                    zombieController.Target = connectedFence.transform;
                else
                    zombieController.Target = 
                        ReferenceManager.instance.GetNearestPlayer(zombieController.transform.position).transform;
            }
        }
    }

    public void OnTriggerExit(Collider other) {

        ZombieController zombieController = other.GetComponent<ZombieController>();
        if (zombieController) {
            zombieController.IsInSpawn = false;
            zombieController.Target = null;
        }
    }
}
