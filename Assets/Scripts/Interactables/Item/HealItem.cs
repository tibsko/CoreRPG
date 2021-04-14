using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : CollactableItem
{
    [SerializeField] int healAmount;

    public void Heal(GameObject player) {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health) {
            health.HealHealth(healAmount);
        }

    }
    public override void Use(GameObject player) {
        Heal(player);
        Destroy(gameObject);
    }
}
