using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] GameObject[] weaponPrefabs;
    [SerializeField] Transform weaponSlot;
    private int activeWeapon = 0;
    List<GameObject> equipedWeapons;
    // Start is called before the first frame update
    void Start()
    {
        equipedWeapons = new List<GameObject>();
        foreach (var weapon in weaponPrefabs)
        {
            GameObject go = Instantiate(weapon, weaponSlot.position, Quaternion.identity, weaponSlot);
            go.layer = LayerMask.NameToLayer("Player");
            equipedWeapons.Add(go);
        }
        EquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            activeWeapon++;
            EquipWeapon();
        }
    }

    public void EquipWeapon()
    {
        if (activeWeapon >= equipedWeapons.Count)
        {
            activeWeapon = 0;
        }
        DisplayWeapon();
    }
    void DisplayWeapon()
    {
        for (int i = 0; i < equipedWeapons.Count; i++)
        {
            equipedWeapons[i].SetActive(false);
        }
        equipedWeapons[activeWeapon].SetActive(true);
    }

    public void AimAndShoot(float rotation) {

    }

    public void Shoot()
    {

    }
}
