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
    }
    public override void Interact() {
        base.Interact();
        RepairDoor();
        
    }

    private void RepairDoor() {
        Debug.Log("repairing");
        doorHealth.HealHealth(20);
    }
}
