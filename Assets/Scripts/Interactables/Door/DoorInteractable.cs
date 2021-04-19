using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public DoorBoard[] doorBoards;
    private DoorHealth doorHealth;

    void Start() {
        doorBoards = gameObject.GetComponentsInChildren<DoorBoard>();
        doorHealth = gameObject.GetComponent<DoorHealth>();
        for (int i = 0; i < doorBoards.Length; i++) {
            doorBoards[i].isActive = true;
        }
    }
    public override void Interact(GameObject player) {
        base.Interact(player);
        RepairDoor();
        
    }

    private void RepairDoor() {
        Debug.Log("repairing");
        doorHealth.HealHealth(20);
    }
}
