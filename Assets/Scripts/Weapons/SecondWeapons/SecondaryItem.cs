using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryItem
{
    public Secondary secondary;
    public int amount;

    public SecondaryItem (Secondary sw,int number) {
        this.secondary = sw;
        this.amount = number;
    }
}
