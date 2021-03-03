using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public AnimatorOverrideController overideAnimator;
    public float damages;
    public float fireRate;
    public float force = 10f;
    public int nbBulletToShoot = 1;
    public float maxDistance;
    public EWeaponType type;
    public enum EWeaponType { Ranged, Melee, Throwable }
   
}
