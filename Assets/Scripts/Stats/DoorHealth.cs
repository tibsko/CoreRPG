using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DoorHealth : CharacterHealth {

    [SerializeField] DoorInteractable doorInteractable;
    private int nbActiveDoor;
    //private NavMeshObstacle obstacle;
    void Start() {
        base.Start();
    }
    // Update is called once per frame
    void Update() {
        base.Update();
        nbActiveDoor = (int)Mathf.Round(currentHealth / doorInteractable.healthStep);
        if (nbActiveDoor > doorInteractable.nbDoor) {
            nbActiveDoor = doorInteractable.nbDoor;
        }
        if (currentHealth >0.0f&& currentHealth<doorInteractable.healthStep) {
            nbActiveDoor = 1;
        }
        for (int i = 0; i < doorInteractable.nbDoor; i++) {
            for (int j = 0; j < nbActiveDoor; j++) {
                doorInteractable.doorBoards[j].isActive = true;
            }
            for (int k = nbActiveDoor; k < doorInteractable.nbDoor; k++) {
                doorInteractable.doorBoards[k].isActive = false;
            }
        }
    }

}


