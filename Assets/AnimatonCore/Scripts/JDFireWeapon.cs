using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JDFireWeapon : MonoBehaviour {
    [Header("Stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    [Header("Set up")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Sound")]
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
    [Header("Graphics")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI text;

    //bools 
    bool shooting, readyToShoot, reloading;


    private void Awake() {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update() {
        Fire();
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void Fire() {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot() {
        readyToShoot = false;

        //Spread & diraction
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = firePoint.transform.forward + new Vector3(x, y, 0);

        //Instantiate bullet
        GameObject bulletGo = Instantiate(bulletPrefab.gameObject, firePoint.position, Quaternion.Euler(direction.normalized));
        Rigidbody myRigidBody = bulletGo.GetComponent<Rigidbody>();
        myRigidBody.AddForce(bulletGo.transform.forward * 10, ForceMode.Impulse);

        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, firePoint.position, Quaternion.identity);

        //Update bullets
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot() {
        readyToShoot = true;
    }

    private void Reload() {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
