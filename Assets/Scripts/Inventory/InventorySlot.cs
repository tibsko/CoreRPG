using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    public Button removeButton;
    public SecondWeapon secondWeapon;
    public int nbItem;

    private Inventory inventory;

    [SerializeField] Text textNumber;



    private void Start() {
        var parent = GetComponentInParent<InventoryUI>();
        inventory = parent.inventory;
        textNumber = GetComponentInChildren<Text>();
    }

    public void AddItem(SecondWeapon newWeapon) {
        //if (!secondWeapon) {
            secondWeapon = newWeapon;
            icon.sprite = secondWeapon.icon;
            icon.enabled = true;
            removeButton.interactable = true;
            nbItem = newWeapon.amunitions;
            textNumber.text =  newWeapon.amunitions.ToString();
        //}
        
    }

    public void StackItem(SecondWeapon weapon) {
        nbItem += weapon.amunitions;
        textNumber.text = nbItem.ToString();
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
            secondWeapon.Use();
        }
    }
}
