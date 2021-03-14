using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    
    // Update is called once per frame
    void Update()
    {
        if(currentHealth<=0) {
            Destroy(gameObject);
        }
    }

}
