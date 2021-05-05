using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    public Button removeButton;
    public SecondWeaponItem secondWeapon;
    public int nbItem;

    private Inventory inventory;

    [SerializeField] Text textNumber;



    private void Start() {
        var parent = GetComponentInParent<InventoryUI>();
        inventory = parent.inventory;
        textNumber = GetComponentInChildren<Text>();
    }

    public void AddItem(SecondWeaponItem newWeapon) {
        secondWeapon = new SecondWeaponItem(newWeapon.weapon,newWeapon.amount) ;
        icon.sprite = secondWeapon.weapon.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        nbItem = newWeapon.amount;
        textNumber.text = newWeapon.amount.ToString();

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
        Debug.Log(inventory.secondWeaponsItems.Count);
            inventory.SetActiveWeapon(secondWeapon);
        }
    }
}
