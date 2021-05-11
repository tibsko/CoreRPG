using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GenericHealth {
    // Update is called once per frame
    GameObject healEffect;
    public void Die() {
        Debug.Log("you are dead");
    }

    
    public override void Heal(int heal, GameObject source) {
        base.Heal(heal, source);
        HealItem healItem = source.GetComponent<HealItem>();
        if (healItem) {
            healEffect = Instantiate(healItem.healParticule, gameObject.transform);
            Destroy(healEffect, 3);
        }
    }
}
