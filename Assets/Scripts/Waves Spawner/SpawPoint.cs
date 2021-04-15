using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawPoint : MonoBehaviour {
    public void OnTriggerExit(Collider enemy) {

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController) {
            enemyController.isInside = true;
        }

    }

}
