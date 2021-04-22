using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopArmInteractable : Interactable
{
    [SerializeField] Weapon weapon;
    [SerializeField] int weaponPrice;

    private bool shophasInteracted;

    public override void Interact(GameObject player) {
        base.Interact(player);
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
