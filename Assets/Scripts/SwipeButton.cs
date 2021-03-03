using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeButton : MonoBehaviour
{
    public PlayerWeapons playerWeapons;
    
    public void SwipeWeapons()
    {

        playerWeapons.activeWeapon++;
        playerWeapons.EquipWeapon();
    }
}
