﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    TakeDamage(20);
        //}
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
