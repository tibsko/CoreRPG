using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWeaponItem
{
    public SecondWeapon weapon;
    public int amount;

    public SecondWeaponItem (SecondWeapon sw,int number) {
        this.weapon = sw;
        this.amount = number;
    }
}
