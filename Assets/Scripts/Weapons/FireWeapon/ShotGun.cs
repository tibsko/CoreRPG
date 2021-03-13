using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : FireWeapon {
    private ShotGunData shotGunData;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (this.FireWeaponData.GetType() == typeof(ShotGunData))
            shotGunData = FireWeaponData as ShotGunData;
        else {
            Debug.LogError("Wrong WeaponData Type in " + this.name);
        }
    }

    public override void Attack() {
        float angle = shotGunData.shotAngle / NbBulletToShoot;
        for (int i = 0; i < NbBulletToShoot; i++) {
            Vector3 bulletRotation = new Vector3(0, (i*angle)-shotGunData.shotAngle/2, 0)+ firePoint.rotation.eulerAngles;
            bulletRotation = new Vector3(0, bulletRotation.y, 0);
            GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(bulletRotation));
            bulletGo.layer = gameObject.layer;
            Bullet bullet = bulletGo.GetComponent<Bullet>();
            bullet.weaponData = FireWeaponData;
            Rigidbody myRigidBody = bulletGo.GetComponent<Rigidbody>();
            myRigidBody.AddForce(bulletGo.transform.forward * FireWeaponData.propulsionForce, ForceMode.Impulse);
        }

        
    }

    public override bool CanAttack() {
        return NbBulletsShooted < FireWeaponData.nbBulletToShoot && Cooldown <= 0;
    }
}
