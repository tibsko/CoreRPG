using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<SecondaryItem> secondaryItems = new List<SecondaryItem>();
    public List<Secondary> secondaries = new List<Secondary>();

    public int Space;

    public Secondary ActiveSecondary;
    public SecondaryItem ActiveSecondaryItem;

    [SerializeField] Transform secondarySlot;

    private bool exist;
    public bool Add(Secondary weapon, int amount) {
        foreach (SecondaryItem sp in secondaryItems) {
            if (sp.secondary.secondaryType == weapon.secondaryType) {
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
            if (secondaryItems.Count >= Space) {
                Debug2.Log("Not enough room in the inventory");
                return false;
            }

            SecondaryItem secondWeaponItem = new SecondaryItem(weapon, amount);
            GameObject goSecondWeapon = EquipWeapon(secondWeaponItem.secondary);
            secondWeaponItem.secondary = goSecondWeapon.GetComponent<Secondary>();
            secondaryItems.Add(secondWeaponItem);
            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }


        return true;
    }
    public void SetActiveSecondary(SecondaryItem swi) {
        for (int i = 0; i < secondaries.Count; i++) {
            secondaries[i].gameObject.SetActive(false);
        }
        ActiveSecondary = swi.secondary;
        ActiveSecondaryItem = swi;
        foreach (Secondary sw in secondaries) {
            if (sw.secondaryType == swi.secondary.secondaryType) {

                sw.gameObject.SetActive(true);
                MeshRenderer[] renderers = sw.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer ren in renderers) {
                    ren.enabled = sw.display;
                }
                if (sw.gameObject.GetComponent<MachineGunAuto>())
                    sw.gameObject.GetComponent<MachineGunAuto>().enabled = sw.display;
                break;
            }
        }

    }
    public void looseBullet() {
        foreach (SecondaryItem sw in secondaryItems) {
            if (sw.secondary.secondaryType == ActiveSecondaryItem.secondary.secondaryType) {
                sw.amount -= 1;
                onItemChangedCallback.Invoke();
                if (sw.amount <= 0) {
                    ActiveSecondaryItem = null;
                    ActiveSecondary = null;
                    for (int i = 0; i < secondaries.Count; i++) {
                        if (sw.secondary.secondaryType == secondaries[i].secondaryType) {
                            secondaries.Remove(secondaries[i]);
                            secondaryItems.Remove(secondaryItems[i]);
                        }
                    }
                   

                }
                break;
            }
        }
    }
    public GameObject EquipWeapon(Secondary secondWeapon) {

        Transform secondarySlotTemp;
        secondarySlotTemp = this.secondarySlot;
        GameObject go = Instantiate(secondWeapon.gameObject, secondarySlotTemp);
        //ParabolicProjectile parabolicProjectile = go.GetComponent<ParabolicProjectile>();
        //if (parabolicProjectile)
        //    parabolicProjectile.enabled = false;
        go.SetActive(false);
        go.layer = gameObject.layer;
        secondaries.Add(go.GetComponent<Secondary>());
        return go;
    }

    public void Remove(SecondaryItem weaponItem) {
        Debug2.Log(weaponItem.amount);
        Destroy(weaponItem.secondary.gameObject);
        secondaries.Remove(weaponItem.secondary);
        secondaryItems.Remove(weaponItem);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
