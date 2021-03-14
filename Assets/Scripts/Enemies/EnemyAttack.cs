using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] FireWeapon fireweapon;
    [SerializeField] Transform weaponSlot;
    [SerializeField] bool activateHitBoxes = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision) {
        PlayerShooter player = collision.GetComponent<PlayerShooter>();
        if (player) {
            transform.LookAt(player.transform.position);
            fireweapon.Attack();
        }
    }
}
