using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public List<SecondWeapon> secondWeapons = new List<SecondWeapon>();
    public int space = 20;

    public bool Add(SecondWeapon weapon) {
        if (!weapon.isDefaultweapon) {
            if (secondWeapons.Count >= space) {
                Debug.Log("Not enough room in the inventory");
                return false;
            }
            secondWeapons.Add(weapon);
            Debug.Log("second weapon added");
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
