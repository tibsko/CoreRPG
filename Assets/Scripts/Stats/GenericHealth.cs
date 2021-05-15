using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GenericHealth : MonoBehaviour {
    public int maxHealth;
    public float CurrentHealth { get; private set; }

    public HealthBar healthBar;

    public UnityEvent onDie;
    public UnityEvent onHeal;
    public UnityEvent onHit;

    public float timerHealth = .2f;

    private List<DamageSource> damageSources;
    // Start is called before the first frame update
    public virtual void Start() {
        CurrentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        damageSources = new List<DamageSource>();
    }

    void Update() {
        List<DamageSource> temp = new List<DamageSource>();
        foreach (DamageSource source in damageSources) {
            source.timerDamage -= Time.deltaTime;
            if (source.timerDamage <= 0.0) {
                temp.Add(source);
            }
        }

        foreach (DamageSource source in temp) {
            damageSources.Remove(source);
        }
    }

    public virtual void Heal(int heal, GameObject source) {
        CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0, maxHealth);
        healthBar.SetHealth(CurrentHealth);
    }

    public void TakeDamage(float damage, GameObject source) {
        if (CurrentHealth <= 0)
            return;

        if (damageSources.Find(x => x.go == source) == null) {
            damageSources.Add(new DamageSource(source, timerHealth));
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
            healthBar.SetHealth(CurrentHealth);
            if (CurrentHealth <= 0) {
                onDie.Invoke();
            }
        }
    }
}

class DamageSource {
    public GameObject go;
    public float timerDamage;

    public DamageSource(GameObject gameObject, float timer) {
        go = gameObject;
        timerDamage = timer;
    }
}