using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    public WeaponData weaponData;
    [HideInInspector] public int nbBulletsShooted = 0;

    //private float timeShoot = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void Shoot()
    {
        if (weaponData.type == WeaponData.EWeaponType.Ranged)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.layer = gameObject.layer;
            Rigidbody myRigidBody = bullet.GetComponent<Rigidbody>();
            Vector3 direction = firePoint.forward;
            direction.y = 0;
            myRigidBody.AddForce(direction * weaponData.force, ForceMode.Impulse);
            //nbBulletsShooted += 1;
        }
    }

    public bool CanShoot()
    {
        return nbBulletsShooted < weaponData.nbBulletToShoot;
    }
}

