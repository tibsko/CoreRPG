using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public override void Interact() {
        base.Interact();
        RepairDoor();
    }

    private void RepairDoor() {
        Debug.Log("repairing");
    }
}
