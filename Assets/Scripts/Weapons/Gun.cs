using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    private GunData gunData;
    // Start is called before the first frame update
    void Start() {
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
        base.Shoot();

        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGo.layer = gameObject.layer;
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.weaponData = weaponData;
        Rigidbody myRigidBody = bulletGo.GetComponent<Rigidbody>();
        myRigidBody.AddForce(bulletGo.transform.forward * weaponData.propulsionForce, ForceMode.Impulse);
    }


}

