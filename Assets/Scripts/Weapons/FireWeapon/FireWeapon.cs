using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireWeapon : Weapon {

    //public FireWeaponData FireWeaponData { get { return weaponData as FireWeaponData; } }

    [Space]
    [Header("Stats")]
    [Space]
    [Header("--FIREWEAPON--")]
    public int magazineSize;
    public float reloadTime;
    public float timeBetweenBurst;
    public float timeBetweenShots;
    public float delayBeforeShoot;
    public bool automatic;

    [Space]
    [Header("Bullet")]
    public int bulletsPerTap;
    public float spread;
    public float range;
    public float velocity;
    public float bulletLifeTime;
    public bool instantiateInFirePoint;

    [Space]
    [Header("Set up")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    [Space]
    [Header("Sound")]
    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip emptySound;

    [Space]
    [Header("Graphics")]
    [SerializeField] GameObject muzzleFlashPrefab;


    //private
    private int bulletsLeft, bulletsToShoot;
    private bool readyToShoot, reloading, emptySignal;
    private AudioSource audioSource;
    private PlayerAttack parentPlayer;
    private Animator animator;

    private void Start() {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        parentPlayer = GetComponentInParent<PlayerAttack>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        //SetText
        //text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void Fire() {
        //if (automatic)
        //    shooting = Input.GetKey(KeyCode.Mouse0);
        //else
        //    shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //if (Input.GetKeyUp(KeyCode.Mouse0))
        //    emptySignal = false;

        //Shoot
        if (readyToShoot && !reloading && bulletsToShoot >= 0) {
            if (bulletsLeft > 0) {

                if (bulletsLeft > bulletsToShoot)
                    bulletsToShoot = bulletsPerTap;
                else
                    bulletsToShoot = bulletsLeft;

                //If only one shot action
                if (timeBetweenShots <= 0f) {
                    animator.SetTrigger("Shoot");
                    Instantiate(muzzleFlashPrefab, firePoint);
                    audioSource.PlayOneShot(shotSound);
                }

                Shoot();
            }
            else
                EmptySignal();
        }
    }

    private void Shoot() {
        readyToShoot = false;

        //Spread & direction
        float randomRotation = Random.Range(-spread, spread);

        //Compute bullet rotation
        Vector3 rotation = parentPlayer.transform.rotation.eulerAngles;
        rotation.y += randomRotation;

        if (!instantiateInFirePoint) {
            //Instantiate bullet
            GameObject bulletGo = Instantiate(bulletPrefab.gameObject, firePoint.position, Quaternion.identity);
            bulletGo.GetComponent<Bullet>().InitializeBullet(rotation, damages, velocity, range, bulletLifeTime);
        }
        else {
            GameObject bulletGo = Instantiate(bulletPrefab.gameObject, firePoint);
            bulletGo.GetComponent<Bullet>().InitializeBullet(rotation, damages, velocity, range, bulletLifeTime);
        }

        //Sound
        if (timeBetweenShots > 0f) {
            animator.SetTrigger("Shoot");
            audioSource.PlayOneShot(shotSound);
            if (muzzleFlashPrefab)
                Instantiate(muzzleFlashPrefab, firePoint);
        }

        //Update bullets
        bulletsLeft--;
        bulletsToShoot--;

        //Call reset shot for last bullet
        if (bulletsToShoot <= 0)
            Invoke(nameof(ResetShot), timeBetweenBurst);

        if (bulletsToShoot > 0 && bulletsLeft > 0)
            Invoke(nameof(Shoot), timeBetweenShots);
    }
    private void ResetShot() {
        readyToShoot = true;
        animator.SetBool("IsAiming", false);
        onEndAttack.Invoke();
        IsAttacking = false;
    }

    private void Reload() {
        reloading = true;
        audioSource.PlayOneShot(reloadSound);

        Invoke(nameof(ReloadFinished), reloadTime);
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

    public override void Attack() {
        IsAttacking = true;
        animator.SetBool("IsAiming", true);
        Invoke(nameof(Fire), delayBeforeShoot);
    }

    public override bool CanAttack() {
        return true;
    }
}
