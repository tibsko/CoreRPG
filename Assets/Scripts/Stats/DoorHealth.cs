using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DoorHealth : GenericHealth {

    [SerializeField] Fence doorInteractable;
    private int nbActiveDoor;

    //private NavMeshObstacle obstacle;
    public override void Start() {
        base.Start();
        doorInteractable = GetComponent<Fence>();
    }
    // Update is called once per frame

    public void UpdateDoorboards() {
        if (currentHealth == maxHealth) {
            nbActiveDoor = doorInteractable.nbDoor;
        }
        else if (currentHealth <= 0) {
            nbActiveDoor = 0;
        }
        else {
            nbActiveDoor = (int)Mathf.Round(currentHealth / doorInteractable.healthStep + 0.7f);
        }

        for (int i = 0; i < doorInteractable.nbDoor; i++) {
            for (int j = 0; j < nbActiveDoor; j++) {
                doorInteractable.doorBoards[j].IsActive = true;
            }
            for (int k = nbActiveDoor; k < doorInteractable.nbDoor; k++) {
                doorInteractable.doorBoards[k].IsActive = false;
            }
        }
    }

}


