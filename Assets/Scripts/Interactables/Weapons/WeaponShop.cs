using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour {

    [SerializeField] Weapon weapon;
    [SerializeField] int weaponPrice;

    public void Buy(GameObject player) {
        PlayerWeapons playerWeapons = player.GetComponent<PlayerWeapons>();
        PlayerMoney playerMoney = player.GetComponent<PlayerMoney>();
        if (playerWeapons && playerMoney && playerMoney.currentMoney >= weaponPrice) {
            playerWeapons.AddWeapon(weapon);
            playerMoney.LooseMoney(weaponPrice);
        }
    }
}
