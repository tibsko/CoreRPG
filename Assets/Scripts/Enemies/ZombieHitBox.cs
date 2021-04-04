﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    [SerializeField] EnemyAttack enemyAttack;
    void OnTriggerEnter(Collider collision) {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth) {

            playerHealth.TakeDamage(enemyAttack.attackDamages);
        }
    }
}
