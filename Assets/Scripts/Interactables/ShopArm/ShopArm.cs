using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopArm : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] int weaponPrice;

    private bool shophasInteracted;

    public void Buy(GameObject player) {
        if (!shophasInteracted) {
            PlayerWeapons playerWeapons = player.GetComponent<PlayerWeapons>();
            PlayerMoney playerMoney = player.GetComponent<PlayerMoney>();
            if (playerWeapons && playerMoney && playerMoney.currentMoney >= weaponPrice) {
                playerWeapons.AddWeapon(weapon);
                playerMoney.LooseMoney(weaponPrice);
                shophasInteracted = true;
            }
        }
    }
}
