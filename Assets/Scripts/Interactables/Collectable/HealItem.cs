using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : CollectableItem
{
    public GameObject healParticule;// cette varible la s'affiche pas je fais comment?

    [SerializeField] int healAmount;


    public void Heal(GameObject player) {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health) {
            health.Heal(healAmount, gameObject);
        }
    }

    public override void Use(GameObject player) {
        Heal(player);
        Destroy(gameObject);
    }
}
