using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    [SerializeField] int nbMaxWeapon = 5;
    [SerializeField] Weapon[] weaponPrefabs;
    [SerializeField] Transform weaponSlot;

    public int activeWeaponIndex = 0;
    public List<Weapon> equipedWeapons;


    public Weapon ActiveWeapon { get { return equipedWeapons[activeWeaponIndex]; } }
    // Start is called before the first frame update
    void Start()
    {
        equipedWeapons = new List<Weapon>();

        foreach (var weaponPrefab in weaponPrefabs) {
            GameObject go = Instantiate(weaponPrefab.gameObject, weaponSlot.position, Quaternion.identity, weaponSlot);
            go.layer = gameObject.layer;

            Weapon weapon = go.GetComponent<Weapon>();
            if (weapon) {
                equipedWeapons.Add(weapon);
            }
            else
                Debug.LogError($"Weapon script is missing on {go.name}");
        }

        EquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(Weapon weapon) {
        if (equipedWeapons.Count < nbMaxWeapon) {
            GameObject go = Instantiate(weapon.gameObject, weaponSlot.position, transform.rotation, weaponSlot);
            go.SetActive(false);
            go.layer = gameObject.layer;
            equipedWeapons.Add(go.GetComponent<Weapon>());
            activeWeaponIndex = equipedWeapons.Count-1;
           
        }
        else {
            Weapon activeWeapon = GetActiveWeapon();
            RemoveWeapon(activeWeapon);
            GameObject go = Instantiate(weapon.gameObject, weaponSlot.position, transform.rotation, weaponSlot);
            go.SetActive(false);
            go.layer = gameObject.layer;
            equipedWeapons.Add(go.GetComponent<Weapon>());
            activeWeaponIndex = equipedWeapons.Count-1;

        }
        EquipWeapon();
    }
    public void RemoveWeapon(Weapon weapon) {
        equipedWeapons.Remove(weapon);
        Destroy(weapon.gameObject);
    }


    public void NextWeapon() {
        activeWeaponIndex++;
        Debug.Log(equipedWeapons.Count);
        if (activeWeaponIndex >= equipedWeapons.Count) {
            activeWeaponIndex = 0;
        }
        EquipWeapon();
    }

    public void EquipWeapon() {
        DisplayActiveWeapon();
        ChangeAnimation(GetActiveWeapon().GetAnimatorOverride());
    }

    void DisplayActiveWeapon() {
        for (int i = 0; i < equipedWeapons.Count; i++) {
            equipedWeapons[i].gameObject.SetActive(false);
        }
        equipedWeapons[activeWeaponIndex].gameObject.SetActive(true);
    }
    public Weapon GetActiveWeapon() {
        return equipedWeapons[activeWeaponIndex].GetComponent<Weapon>();
    }
    private void ChangeAnimation(AnimatorOverrideController animatorOverrideController) {
        //animator.runtimeAnimatorController = animatorOverrideController;
    }
}
