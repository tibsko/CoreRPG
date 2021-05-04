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
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.secondWeapons.Count) {
                slots[i].AddItem(inventory.secondWeapons[i]);
            }
            else {
                slots[i].ClearSLot();
            }
        }
    }
}
