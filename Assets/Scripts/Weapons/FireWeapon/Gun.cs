using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    private GunData gunData;
    // Start is called before the first frame update
    void Start() {
        base.Start();
        if (weaponData.GetType() == typeof(GunData))
            gunData = weaponData as GunData;
        else {
            Debug.LogError("Wrong WeaponData Type in " + this.name);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public override void Shoot() {
        

        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGo.layer = gameObject.layer;
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.weaponData = FireWeaponData;
        Rigidbody myRigidBody = bulletGo.GetComponent<Rigidbody>();
        myRigidBody.AddForce(bulletGo.transform.forward * FireWeaponData.propulsionForce, ForceMode.Impulse);
    }

    public override bool CanShoot() {
        return nbBulletsShooted < FireWeaponData.nbBulletToShoot && playerRateTimer <= 0;
    }
}

