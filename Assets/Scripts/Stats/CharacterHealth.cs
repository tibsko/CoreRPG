using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    public UnityEvent onDie;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void HealHealth(int heal) {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage) {
        if (currentHealth < 0)
            return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) {
            onDie.Invoke();
        }
    }

}
