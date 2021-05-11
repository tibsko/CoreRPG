using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryPickUp : MonoBehaviour
{
    [SerializeField] Secondary secondary;
    public int quantity;

    public void PickUp(GameObject player) {
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory) {
            bool wasPickedUp = inventory.Add(secondary,quantity);
            if (wasPickedUp) {
                Destroy(gameObject);
                HUD.instance.ActivateButton(false);
            }
        }
        else
            Debug.LogError($"Can't find inventory in {player.name}");
    }
}
