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
    public Animator animator;

    [Header("Sound")]
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;

    [Header("Graphics")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI text;

    //private
    bool shooting, readyToShoot, reloading, emptySignal;
    AudioSource audioSource;

    private void Start() {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
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

        if (Input.GetKeyUp(KeyCode.Mouse0))
            emptySignal = false;

        //Shoot
        if (readyToShoot && shooting && !reloading) {
            if (bulletsLeft > 0) {
                bulletsShot = bulletsPerTap;
                Shoot();
                animator.SetTrigger("Shoot");
            }
            else
                EmptySignal();

        }
    }
    private void Shoot() {
        readyToShoot = false;

        //Spread & diraction
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = firePoint.transform.forward + new Vector3(x, y, 0);

        //Instantiate bullet
        GameObject bulletGo = Instantiate(bulletPrefab.gameObject, firePoint.position, firePoint.rotation);

        //Sound
        audioSource.PlayOneShot(shotSound);

        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, firePoint);

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
        audioSource.PlayOneShot(reloadSound);

        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void EmptySignal() {
        if (!emptySignal) {
            emptySignal = true;
            audioSource.PlayOneShot(emptySound);
        }
    }
}
