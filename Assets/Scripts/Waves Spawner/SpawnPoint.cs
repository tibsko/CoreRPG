using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    public void OnTriggerExit(Collider enemy) {

        ZombieController enemyController = enemy.GetComponent<ZombieController>();
        if (enemyController) {
            enemyController.isInside = true;
        }

    }

}
