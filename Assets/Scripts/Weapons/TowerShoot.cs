using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    Transform player;
    
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float force = 10f;
    [SerializeField] float radiusAutoShootTower = 4f;

    public float shootRate = 2f;
    private float nextShoot;
    private Vector3 forceVector;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusAutoShootTower);
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //playerIsDetected = true;
                player = collider.GetComponent<Transform>();
                AutoShootTower();
            }
            else
            {
                //playerIsDetected = false;
            }
        }
    }

    void AutoShootTower()
    {

        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + shootRate;
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            Rigidbody myRigidBody = bullet.GetComponent<Rigidbody>();
            float distance = Vector3.Distance(player.position, firepoint.position);
            forceVector = new Vector3((player.position.x - firepoint.position.x)*force, distance*0.1f*force, (player.position.z - firepoint.position.z)*force);
            myRigidBody.AddForce(forceVector, ForceMode.Impulse);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusAutoShootTower);
    }
}
