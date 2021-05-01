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
    
    public void UpdateDoorboards() {
        if (CurrentHealth ==maxHealth) {
            nbActiveDoor = doorInteractable.nbDoor;
        }
        else if (currentHealth <=0) {
            nbActiveDoor = 0;
        }
        else {
            nbActiveDoor = (int)Mathf.Round(CurrentHealth / doorInteractable.healthStep+0.7f);
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


