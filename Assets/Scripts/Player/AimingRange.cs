using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRange : MonoBehaviour
{
    private PlayerShoot playerShoot;
    private SpriteRenderer aimingRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerShoot = GetComponentInParent<PlayerShoot>();
        aimingRenderer = GetComponent<SpriteRenderer>();
        aimingRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerShoot == null) {
            return;
        }

        if (playerShoot.isAiming)
        {
            aimingRenderer.enabled = true;
            RotateAiming();
        }
        else if (!playerShoot.isAiming)
        {
            aimingRenderer.enabled = false;
        }
    }

    void RotateAiming()
    {
        transform.LookAt(playerShoot.transform.position + playerShoot.xzAim);
        Vector3 rototo = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(90, rototo.y + 90, 0);

    }
}
