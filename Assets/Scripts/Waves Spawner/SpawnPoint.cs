using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public void OnTriggerExit(Collider other) {

        ZombieController zombieController = other.GetComponent<ZombieController>();
        if (zombieController) {
            zombieController.IsInRoom = true;
        }
    }
}
