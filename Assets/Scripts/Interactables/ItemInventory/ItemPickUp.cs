using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {
    public Item item;
    public override void Interact(GameObject player) {
        base.Interact(player);
        PickUp();
    }

    void PickUp() {
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) {
            Destroy(gameObject);
            HUD.instance.ActivateButton(false);
            Debug.Log("Picking Up " + item.name);
        }
    }
}
