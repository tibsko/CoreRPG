using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableSecondary : Secondary {
    [SerializeField] int healthAmount;
    [SerializeField] GameObject healEffect;
    private PlayerController player;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start() {
        player = ReferenceManager.instance.GetNearestPlayer(transform.position);
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
    }

    public override void Attack() {
        base.Attack();
        Debug2.Log("heal" + healthAmount);
        playerHealth.Heal(healthAmount, gameObject);
        GameObject fx = Instantiate(healEffect, player.transform.position, Quaternion.identity);
        Destroy(fx, 2f);


    }
    public override void OnRelease(Vector2 aim) {
        Attack();
    }
}
