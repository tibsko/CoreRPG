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
        //inventory.onItemChangedStackCallback += UpdateStackUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI() {
        for (int i = 0; i < inventory.secondWeapons.Count; i++) {
            if (i < inventory.secondWeapons.Count) {
                slots[i].AddItem(inventory.secondWeapons[i]);
            }

        }
        //for (int i = 0; i < slots.Length; i++) {
        //    if (i < inventory.secondWeapons.Count) {

        //        else {
        //            if (slots[i].secondWeapon.secondWeaponType != inventory.secondWeapons[i].secondWeaponType) {
        //                slots[i].AddItem(inventory.secondWeapons[i]);
        //            }
        //            else {

        //            }

        //        }
        //    }
        //else {
        //    slots[i].ClearSLot();
        //}
    }
}

//void UpdateStackUI() {
//    Debug.Log("inUpdate");
//    for (int i = 0; i < slots.Length; i++) {

//        if (slots[i].secondWeapon.secondWeaponType == inventory.secondWeapons[i].secondWeaponType) {
//            slots[i].secondWeapon.nbShoot+=inventory;
//        }


//    }

