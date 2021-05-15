using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public void OnTriggerExit(Collider other) {

        WalkerController zombieController = other.GetComponent<WalkerController>();
        if (zombieController) {
            zombieController.isInRoom = true;
        }
    }
}
