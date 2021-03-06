using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapButton : MonoBehaviour
{
    public PlayerShoot playerWeapons;
    
    public void SwapWeapons()
    {
        playerWeapons.NextWeapon();
    }
}
