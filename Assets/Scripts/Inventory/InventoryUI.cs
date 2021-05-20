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
        for (int i = 0; i < inventory.Space; i++) {
            if (i < inventory.secondaryItems.Count) {
                slots[i].AddItem(inventory.secondaryItems[i]);
            }
        }
    }
}

