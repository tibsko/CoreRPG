using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    PlayerController player;

    [SerializeField] float fireRate;
    [SerializeField] float force = 10f;

    private int nbBulletToShoot = 1;



    //private float timeShoot = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isShooting && player.nbBulletsShooted < nbBulletToShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody myRigidBody = bullet.GetComponent<Rigidbody>();
        myRigidBody.AddForce(firePoint.forward * force, ForceMode.Impulse);
        player.nbBulletsShooted += 1;
    }
}

