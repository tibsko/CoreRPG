using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {


    public void OnTriggerExit(Collider zombie) {

        ZombieController zombieController = zombie.GetComponent<ZombieController>();
        if (zombieController) {
            zombieController.isInRoom = true;
        }
    }

}
