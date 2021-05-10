using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {
    [SerializeField] Transform secondWeaponSlot; 
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<SecondWeaponItem> secondWeaponsItems=new List<SecondWeaponItem>();
    public List<SecondWeapon> secondWeapons = new List<SecondWeapon>();

    public int space = 3;
    bool exist;

    public SecondWeapon ActiveSecondWeapon;

    public int ActiveSecondWeaponIndex { get; private set; }

    public bool Add(SecondWeapon weapon, int amount) {
        foreach (SecondWeaponItem sp in secondWeaponsItems) {
            if (sp.weapon.secondWeaponType == weapon.secondWeaponType) {
                //sp.weapon = weapon;
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
            GameObject goSecondWeapon = EquipWeapon(secondWeaponItem.weapon);
            secondWeaponItem.weapon = goSecondWeapon.GetComponent<SecondWeapon>();
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
        for (int i = 0; i < secondWeapons.Count; i++) {
            secondWeapons[i].gameObject.SetActive(false);
        }
        ActiveSecondWeapon = swi.weapon;
        Debug.Log(ActiveSecondWeapon);
        foreach (SecondWeapon sw in secondWeapons) {
            if (sw.secondWeaponType == swi.weapon.secondWeaponType) {
                sw.gameObject.SetActive(true);
                break;
            }
        }
       
    }
    public GameObject EquipWeapon(SecondWeapon secondWeapon) {

        Transform weaponSlot;
        weaponSlot= secondWeaponSlot;

        GameObject go = Instantiate(secondWeapon.gameObject, weaponSlot);
        ParabolicProjectile parabolicProjectile = go.GetComponent<ParabolicProjectile>();
        if (parabolicProjectile)
            parabolicProjectile.enabled = false;
        go.SetActive(false);
        go.layer = gameObject.layer;
        secondWeapons.Add(go.GetComponent<SecondWeapon>());
        return go;
    }

    public void Remove(SecondWeaponItem weapon) {
        secondWeaponsItems.Remove(weapon);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
