using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {


    public void OnTriggerExit(Collider zombie) {

        WalkerController zombieController = zombie.GetComponent<WalkerController>();
        if (zombieController) {
            zombieController.isInRoom = true;
        }
    }

}
