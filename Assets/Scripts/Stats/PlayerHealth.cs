using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth {
    // Update is called once per frame
    GameObject healEffect;
    public void Die() {
        Debug.Log("you are dead");
    }

    
    public override void HealHealth(int heal, GameObject source) {
        base.HealHealth(heal, source);
        HealItem healItem = source.GetComponent<HealItem>();
        if (healItem) {
            healEffect = Instantiate(healItem.healParticule, gameObject.transform);
            Destroy(healEffect, 3);
        }
    }
}
