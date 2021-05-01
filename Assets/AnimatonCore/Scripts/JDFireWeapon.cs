using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JDFireWeapon : FireWeapon {
    [Header("Stats")]
    public int damage;
    public float spread, range;
    public float reloadTime, timeBetweenBurst, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool automatic;
    public float delayBeforeShoot;

    [Header("Set up")]
    //public Transform firePoint;
    //public GameObject bulletPrefab;
    public Animator animator;

    [Header("Sound")]
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;

    [Header("Graphics")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI text;

    [HideInInspector] public bool shooting;

    //private
    private int bulletsLeft, bulletsToShot;
    private bool readyToShoot, reloading, emptySignal;
    private AudioSource audioSource;
    private PlayerAttack parentPlayer;

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
        if (readyToShoot && shooting && !reloading && bulletsToShot <= 0) {
            if (bulletsLeft > 0) {

                if (bulletsLeft > bulletsToShot)
                    bulletsToShot = bulletsPerTap;
                else
                    bulletsToShot = bulletsLeft;

                //Sound
                audioSource.PlayOneShot(shotSound);
                Shoot();
                animator.SetTrigger("Shoot");
            }
            else
                EmptySignal();

        }
    }
    private void Shoot() {
        readyToShoot = false;

        //Spread & direction
        float y = Random.Range(-spread, spread);

        //Instantiate bullet
        Vector3 rotation = Vector3.zero;
        rotation.y = y;
        GameObject bulletGo = Instantiate(bulletPrefab.gameObject, firePoint.position, Quaternion.identity);
        bulletGo.GetComponent<JDBullet>().Init(parentPlayer.transform.rotation.eulerAngles + rotation);


        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, firePoint);

        //Update bullets
        bulletsLeft--;
        bulletsToShot--;

        //Call reset shot for last bullet
        if (bulletsToShot <= 0)
            Invoke(nameof(ResetShot), timeBetweenBurst);

        if (bulletsToShot > 0 && bulletsLeft > 0)
            Invoke(nameof(Shoot), timeBetweenShots);
    }
    private void ResetShot() {
        readyToShoot = true;
        onEndAttack.Invoke();
        shooting = false;
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
        shooting = true;
        Invoke(nameof(Fire), delayBeforeShoot);
    }

    public override bool CanAttack() {
        return true;
    }
}
