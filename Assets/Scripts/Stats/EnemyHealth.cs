﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth {

    // Update is called once per frame
    public void Die() {
        Debug.Log("Dead : " + name);
        Destroy(gameObject);
    }

}
