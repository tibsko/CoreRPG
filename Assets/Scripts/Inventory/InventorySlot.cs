using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public SecondaryItem secondaryItem;
    public Secondary secondary;
    public int nbItem;
    public Image icon;

    private Inventory inventory;

    [SerializeField] Text textNumber;



    private void Start() {
        var parent = GetComponentInParent<InventoryUI>();
        inventory = parent.inventory;
        textNumber = GetComponentInChildren<Text>();
    }


    public void AddItem(SecondaryItem newWeapon) {
        if (newWeapon.amount >= 0) {
            secondaryItem = new SecondaryItem(newWeapon.secondary, newWeapon.amount);
            secondary = secondaryItem.secondary;
            icon.sprite = secondaryItem.secondary.icon;
            icon.enabled = true;
            nbItem = newWeapon.amount;
            textNumber.enabled=true;
            textNumber.text = newWeapon.amount.ToString();
            if (nbItem <= 0)
                ClearSLot();
        }

    }

    public void ClearSLot() {
        secondaryItem = null;
        icon.sprite = null;
        icon.enabled = false;
        nbItem = 0;
        textNumber.enabled=false;
    }

    public void OnRemoveButton() {
        inventory.Remove(secondaryItem);
    }

    public void UseItem() {
        if (secondaryItem != null) {
            Debug.Log(inventory.secondaryItems.Count);
            inventory.SetActiveSecondary(secondaryItem);
        }
    }
}
