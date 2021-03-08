using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public FireWeaponData weaponData;
    [HideInInspector] public int nbBulletsShooted = 0;
    [HideInInspector] public int damages;
    [HideInInspector] public float fireRate;
    [HideInInspector] public float propulsionForce;
    [HideInInspector] public int nbBulletToShoot;
    [HideInInspector] public float maxDistance;
    [HideInInspector] public float lifeTimeBullet;

    private float playerRateTimer;


    //private float timeShoot = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        damages = weaponData.damages;
        fireRate = weaponData.attackRate;
        propulsionForce = weaponData.propulsionForce;
        nbBulletToShoot = weaponData.nbBulletToShoot;
        maxDistance = weaponData.maxDistance;
        lifeTimeBullet = weaponData.lifeTimeBullet;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public virtual void Shoot()
    {
        
    }

    public bool CanShoot()
    {
        return nbBulletsShooted < weaponData.nbBulletToShoot && playerRateTimer<=0;
    }
}



