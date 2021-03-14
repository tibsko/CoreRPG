using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireWeaponData : WeaponData {
    public float propulsionForce;
    public int nbBulletToShoot;
    public float maxDistance;
    public float lifeTimeBullet;
}