﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public AnimationClip animation;
    public float damages;
    public float fireRate;
    public float force = 10f;
    public int nbBulletToShoot = 1;
}
