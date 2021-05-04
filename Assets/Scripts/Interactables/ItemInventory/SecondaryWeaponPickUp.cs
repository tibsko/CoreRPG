using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponPickUp : Interactable
{
    [SerializeField] SecondWeapon secondWeapon;

    public override void Interact(GameObject player) {
        base.Interact(player);
        PickUp(player);
    }

    void PickUp(GameObject player) {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory) {
            bool wasPickedUp = inventory.Add(secondWeapon);
            if (wasPickedUp) {
                Destroy(gameObject);
                HUD.instance.ActivateButton(false);
                Debug.Log("Picking Up " + secondWeapon.name);
            }
        }
        else
            Debug.LogError($"Can't find inventory in {player.name}");
    }
}
