using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public Inventory inventory;

    private InventorySlot[] slots;

    void Start() {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
                Debug.Log("Item addes");
            }
            else {
                slots[i].ClearSLot();
            }
        }
    }
}
