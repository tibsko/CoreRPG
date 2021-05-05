using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponPickUp : Interactable
{
    [SerializeField] SecondWeapon secondWeapon;
    public int quantity;

    public override void Interact(GameObject player) {
        base.Interact(player);
        PickUp(player);
    }

    void PickUp(GameObject player) {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory) {
            SecondWeapon newSecondWeapon = Instantiate(secondWeapon);            
            bool wasPickedUp = inventory.Add(newSecondWeapon,quantity);
            if (wasPickedUp) {
                Destroy(gameObject);
                Destroy(newSecondWeapon.gameObject);
                HUD.instance.ActivateButton(false);
            }
        }
        else
            Debug.LogError($"Can't find inventory in {player.name}");
    }
}
