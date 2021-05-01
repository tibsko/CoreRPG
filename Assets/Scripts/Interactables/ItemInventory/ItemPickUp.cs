using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable {
    public Item item;
    public override void Interact(GameObject player) {
        base.Interact(player);
        PickUp(player);
    }

    void PickUp(GameObject player) {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory) {
            bool wasPickedUp = inventory.Add(item);
            if (wasPickedUp) {
                Destroy(gameObject);
                HUD.instance.ActivateButton(false);
                Debug.Log("Picking Up " + item.name);
            }
        }
        else
            Debug.LogError($"Can't find inventory in {player.name}");
    }
}
