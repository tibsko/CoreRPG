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

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody myRigidBody = bullet.GetComponent<Rigidbody>();
        myRigidBody.AddForce(firePoint.forward * weaponData.force, ForceMode.Impulse);
        nbBulletsShooted += 1;
    }

    public bool CanShoot()
    {
        return nbBulletsShooted < weaponData.nbBulletToShoot;
    }
}

