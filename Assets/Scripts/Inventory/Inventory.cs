using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<SecondWeaponItem> secondWeaponsItems=new List<SecondWeaponItem>();
    public int space = 3;
    bool exist;

    public SecondWeaponItem ActiveSecondWeapon;
    public int ActiveSecondWeaponIndex { get; private set; }

    public bool Add(SecondWeapon weapon, int amount) {
        foreach (SecondWeaponItem sp in secondWeaponsItems) {
            if (sp.weapon.secondWeaponType == weapon.secondWeaponType) {
                sp.weapon = weapon;
                sp.amount += amount;
                exist = true;
                break;
            }
            else {
                exist = false;
            }
        }
        if (!exist) {
            if (secondWeaponsItems.Count >= space) {
                Debug.Log("Not enough room in the inventory");
                return false;
            }
            SecondWeaponItem secondWeaponItem = new SecondWeaponItem(weapon,amount);
            secondWeaponsItems.Add(secondWeaponItem);
            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }


        return true;
    }
    public void SetActiveWeapon(SecondWeaponItem swi) {
        ActiveSecondWeapon = swi;
        Debug.Log(ActiveSecondWeapon.weapon);
        //for (int i = 0; i < secondWeaponsItems.Count; i++) {
        //    if (secondWeaponsItems[i].weapon.secondWeaponType == swi.weapon.secondWeaponType) {
        //        ActiveSecondWeaponIndex = i;
        //        Debug.Log(ActiveSecondWeaponIndex);
        //        break;
        //    }
        //}
    }

    public void Remove(SecondWeaponItem weapon) {
        secondWeaponsItems.Remove(weapon);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
