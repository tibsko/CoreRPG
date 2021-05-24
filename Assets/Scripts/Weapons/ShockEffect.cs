using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : MonoBehaviour {
    [SerializeField] float shockDuration;

    private ZombieController zombieController;
    public void Shocked(GameObject enemy) {
        if (enemy.transform.HasComponent(out ZombieController controller)) {
            zombieController = controller;
            zombieController.SetStun(true, shockDuration);
            Debug2.Log(enemy.name + " Shocked", "TDEnemyTouch");
            
        }
    }
   
}
