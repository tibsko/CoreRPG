using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour {

    [SerializeField] Weapon weapon;
    [SerializeField] int weaponPrice;

    public void Buy(GameObject player) {
        PlayerWeapons playerWeapons = player.GetComponentInParent<PlayerWeapons>();
        PlayerMoney playerMoney = player.GetComponentInParent<PlayerMoney>();
        if (playerWeapons && playerMoney && playerMoney.currentMoney >= weaponPrice) {
            Debug2.Log("buy weapon");
            playerWeapons.AddWeapon(weapon);
            playerMoney.LooseMoney(weaponPrice);
        }
    }
}
