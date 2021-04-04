using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) {
            Debug.Log("you are dead");
        }
    }
}
