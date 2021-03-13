using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : FireWeapon {

    private SniperData SniperData;

    // Start is called before the first frame update
    void Start() {
        base.Start();
        if (this.FireWeaponData.GetType() == typeof(SniperData))
            SniperData = FireWeaponData as SniperData;
        else {
            Debug.LogError("Wrong WeaponData Type in " + this.name);
        }
    }


    public override void Attack() {
        Vector3 bulletRotation = new Vector3(0, firePoint.rotation.eulerAngles.y, 0);
        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(bulletRotation));
        bulletGo.layer = gameObject.layer;
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.weaponData = FireWeaponData;
        Rigidbody myRigidBody = bulletGo.GetComponent<Rigidbody>();
        myRigidBody.AddForce(bulletGo.transform.forward * FireWeaponData.propulsionForce, ForceMode.Impulse);
        bullet.damagePerDistance = true;

    }

    public override bool CanAttack() {
        return NbBulletsShooted < FireWeaponData.nbBulletToShoot && Cooldown <= 0;
    }
}
