using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FireWeapon : Weapon {

    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject bulletPrefab;

    public float FireRate { get; protected set; }

    public float BulletForce { get; protected set; }
    public int NbBulletToShoot { get; protected set; }
    public int NbBulletsShooted { get; protected set; }
    public float MaxDistance { get; protected set; }
    public FireWeaponData FireWeaponData { get { return weaponData as FireWeaponData; } }

    public abstract override void Attack();
    public abstract override bool CanAttack();

    protected void Start() {
        base.Start();
        BulletForce = FireWeaponData.propulsionForce;
        NbBulletToShoot = FireWeaponData.nbBulletToShoot;
        MaxDistance = FireWeaponData.maxDistance;
    }
}
