using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    public Button removeButton;

    private SecondWeapon secondWeapon;
    Inventory inventory;

    public int nbItem;



    private void Start() {
        var parent = GetComponentInParent<InventoryUI>();
        inventory = parent.inventory;
    }

    public void AddItem(SecondWeapon newWeapon) {
        if (!secondWeapon) {
            secondWeapon = newWeapon;
            Debug.Log("Item Added");
            icon.sprite = secondWeapon.icon;
            icon.enabled = true;
            removeButton.interactable = true;
            nbItem = newWeapon.nbShoot;
        }
        else {
            if(secondWeapon.secondWeaponType == newWeapon.secondWeaponType) {
                //tu t'es arrét& ici

            }
        }
    }

    public void ClearSLot() {
        secondWeapon = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() {
        inventory.Remove(secondWeapon);
    }

    public void UseItem() {
        if (secondWeapon != null) {
            Debug.Log("Using" + name);
            secondWeapon.Use();
        }
    }
}
