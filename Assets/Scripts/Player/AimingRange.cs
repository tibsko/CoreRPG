using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRange : MonoBehaviour
{
    private PlayerShooter playerShoot;
    private SpriteRenderer aimingRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerShoot = GetComponentInParent<PlayerShooter>();
        aimingRenderer = GetComponent<SpriteRenderer>();
        aimingRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerShoot == null) {
            return;
        }

        if (playerShoot.DisplayAim)
        {
            aimingRenderer.enabled = true;
            RotateAiming();
        }
        else
        {
            aimingRenderer.enabled = false;
        }
    }

    void RotateAiming()
    {
        transform.LookAt(playerShoot.transform.position + playerShoot.AimDirection);
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(90, rotation.y + 90, 0);

    }
}
