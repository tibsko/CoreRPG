using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth {
    // Update is called once per frame
    public void Die() {
        Debug.Log("you are dead");
    }
}
