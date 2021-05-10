using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public SecondWeaponItem secondWeaponItem;
    public SecondWeapon secondWeapon;
    public int nbItem;
    public Image icon;
    public Button removeButton;

    private Inventory inventory;

    [SerializeField] Text textNumber;



    private void Start() {
        var parent = GetComponentInParent<InventoryUI>();
        inventory = parent.inventory;
        textNumber = GetComponentInChildren<Text>();
    }

    public void AddItem(SecondWeaponItem newWeapon) {
        secondWeaponItem = new SecondWeaponItem(newWeapon.weapon,newWeapon.amount) ;
        secondWeapon = secondWeaponItem.weapon;
        icon.sprite = secondWeaponItem.weapon.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        nbItem = newWeapon.amount;
        textNumber.text = newWeapon.amount.ToString();

    }

    public void ClearSLot() {
        secondWeaponItem = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() {
        inventory.Remove(secondWeaponItem);
    }

    public void UseItem() {
        if (secondWeaponItem != null) {
            Debug.Log(inventory.secondWeaponsItems.Count);
            inventory.SetActiveWeapon(secondWeaponItem);
        }
    }
}
