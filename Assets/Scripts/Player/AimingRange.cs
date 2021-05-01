using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRange : MonoBehaviour {
    private PlayerAttack playerShoot;
    //private SpriteRenderer aimingRenderer;
    private LineRenderer line;
    // Start is called before the first frame update
    void Start() {
        playerShoot = GetComponentInParent<PlayerAttack>();
        line = GetComponent<LineRenderer>();
        //aimingRenderer = GetComponent<SpriteRenderer>();
        //aimingRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (playerShoot == null) {
            return;
        }

        if (playerShoot.DisplayAim) {
            line.enabled = true;
            RotateAiming();
        }
        else {
            line.enabled = false;
        }
    }

    void RotateAiming() {
        //transform.LookAt(playerShoot.transform.position + playerShoot.AimDirection);
        //Vector3 rotation = transform.rotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(90, rotation.y + 90, 0);
        line.SetPosition(0, transform.position + Vector3.up * 0.1f);
        line.SetPosition(1, transform.position + Vector3.up * 0.1f + playerShoot.AimDirection * playerShoot.ActiveWeapon.autoshootDistance);
    }
}
