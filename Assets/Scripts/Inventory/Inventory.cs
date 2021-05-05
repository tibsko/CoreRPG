using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnItemStackChanged();
    public OnItemStackChanged onItemChangedStackCallback;
    public List<SecondWeaponItem> secondWeaponsItem = new List<SecondWeapon>();
    public int space = 20;

    public int ActiveSecondWeaponIndex { get; private set; }
    public SecondWeapon ActiveSecondWeapon { get { return secondWeapons[ActiveSecondWeaponIndex]; } }

   

    public bool Add(SecondWeapon weapon) {
        bool exist = false;
        int i = 0;
        if (!weapon.isDefaultweapon) {
            foreach (SecondWeapon sp in secondWeapons) {
                if (sp.secondWeaponType == weapon.secondWeaponType) {
                    exist = true;
                    secondWeapons[i] = weapon;
                    secondWeapons[i].amunitions += weapon.baseAmunitions;
                    //onItemChangedStackCallback.Invoke();
                    break;
                }
                else if (sp.secondWeaponType == weapon.secondWeaponType) {
                    exist = false;
                }
                i += 1;
            }
            if (!exist) {
                if (secondWeapons.Count >= space) {
                    Debug.Log("Not enough room in the inventory");
                    return false;
                }
                secondWeapons.Add(weapon);
                secondWeapons[i].amunitions += weapon.baseAmunitions;
            }
          
            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }

        }
        return true;
    }

    public void Remove(SecondWeapon weapon) {
        secondWeapons.Remove(weapon);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
