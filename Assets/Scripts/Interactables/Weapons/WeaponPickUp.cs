using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] Weapon weapon;
   public void PickUp(GameObject player) {
        PlayerWeapons playerWeapons = player.GetComponentInParent<PlayerWeapons>();
        if (playerWeapons) {
            Debug.Log("Pick Up" + gameObject.name);
            playerWeapons.AddWeapon(weapon);
            player.GetComponentInChildren<PlayerInteraction>().RemoveFocus() ;
            Destroy(gameObject);
        }
    }
}
