﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapButton : MonoBehaviour
{
    public PlayerWeapons playerWeapons;
    
    public void SwapWeapons()
    {
        Debug.Log("Swipe");
        playerWeapons.NextWeapon();
    }
}
