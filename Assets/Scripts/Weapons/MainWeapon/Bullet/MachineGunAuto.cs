using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunAuto : MonoBehaviour {
    [Space]
    [Header("Rotation")]
    [Space]
    [SerializeField] Transform gun;
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask layerMask;
    [Space]
    [Header("Set Up Gun")]
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range;
    [SerializeField] float spread;
    [SerializeField] float velocity;
    [SerializeField] int damages;
    [SerializeField] float bulletLifeTime;
    [SerializeField] float timeBetweenBurst;

    private bool readyToShoot;

    void Start() {
        readyToShoot = true;
    }

    void OnTriggerStay(Collider collider) {
        if (layerMask.ContainsLayer(collider.gameObject.layer)) {
            Rotation(collider.gameObject);
        }
    }
    void Rotation(GameObject enemy) {
        Vector3 lookAtPosition = enemy.transform.position - transform.position;
        lookAtPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookAtPosition);
        gun.rotation = Quaternion.Slerp(gun.rotation, rotation, Time.deltaTime * rotationSpeed);
        if (readyToShoot)
            Attack();
    }

    void Attack() {
        readyToShoot = false;

        //Spread & direction
        float randomRotation = Random.Range(-spread, spread);

        //Compute bullet rotation
        Vector3 rotation = gun.rotation.eulerAngles;
        rotation.y += randomRotation;
        GameObject bulletGo = Instantiate(bulletPrefab.gameObject, firePoint1);
        bulletGo.GetComponent<Bullet>().InitializeBullet(rotation, damages, velocity, range, bulletLifeTime);
        StartCoroutine(SecondShoot());
        Invoke(nameof(ResetShot), timeBetweenBurst);
    }
    IEnumerator SecondShoot() {
        yield return new WaitForSeconds(timeBetweenBurst / 2);

        //Spread & direction
        float randomRotation = Random.Range(-spread, spread);

        //Compute bullet rotation
        Vector3 rotation = gun.rotation.eulerAngles;
        rotation.y += randomRotation;
        GameObject bulletGo2 = Instantiate(bulletPrefab.gameObject, firePoint2);
        bulletGo2.GetComponent<Bullet>().InitializeBullet(rotation, damages, velocity, range, bulletLifeTime);
    }
    private void ResetShot() {
        readyToShoot = true;
    }

}
