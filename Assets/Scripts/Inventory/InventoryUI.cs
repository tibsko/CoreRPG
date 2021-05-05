using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public Inventory inventory;

    private InventorySlot[] slots;

    void Start() {
        inventory = GetComponentInParent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI() {
        for (int i = 0; i < inventory.secondWeaponsItems.Count; i++) {
            if (i < inventory.secondWeaponsItems.Count) {
                slots[i].AddItem(inventory.secondWeaponsItems[i]);
            }
        }
    }
}

