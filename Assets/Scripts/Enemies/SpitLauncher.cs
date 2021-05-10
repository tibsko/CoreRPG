using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitLauncher : MonoBehaviour {

    [SerializeField] GameObject spitBullet;
    [SerializeField] Transform spitPoint;

    private SpitterAttack spitterAttack;

    void Start() {
        spitterAttack = GetComponentInParent<SpitterAttack>();
    }

    public void SpitAttack() {
        ParabolicProjectile projectile = Instantiate(spitBullet, spitPoint.position, Quaternion.identity)
            .GetComponent<ParabolicProjectile>();
        projectile.throwObject = true;
        projectile.SetTarget(spitterAttack.target.position);
    }
}
