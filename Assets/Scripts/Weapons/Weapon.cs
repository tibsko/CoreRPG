using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public WeaponData weaponData;

    [HideInInspector] public int nbBulletsShooted = 0;
    [HideInInspector] public int damages;
    [HideInInspector] public float fireRate;
    [HideInInspector] public float propulsionForce;
    [HideInInspector] public int nbBulletToShoot;
    [HideInInspector] public float maxDistance;
    [HideInInspector] public float lifeTimeBullet;
    
    [HideInInspector] public float playerRateTimer;

    public FireWeaponData FireWeaponData { get; set; }
    public MeleeWeaponData MeleeWeaponData { get; set; }

    //private float timeShoot = 0.2f;


    // Start is called before the first frame update
    protected void Start() {
        FireWeaponData = weaponData as FireWeaponData;
        damages = weaponData.damages;
        fireRate = weaponData.attackRate;
        propulsionForce = FireWeaponData.propulsionForce;
        nbBulletToShoot = FireWeaponData.nbBulletToShoot;
        maxDistance = FireWeaponData.maxDistance;
        lifeTimeBullet = FireWeaponData.lifeTimeBullet;
    }


    public abstract void Shoot();

    public abstract bool CanShoot();
        
    
}



