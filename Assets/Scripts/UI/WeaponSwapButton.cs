using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapButton : MonoBehaviour
{
    public PlayerShooter playerWeapons;
    
    public void SwapWeapons()
    {
        playerWeapons.NextWeapon();
    }
}
