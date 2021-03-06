using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRange : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private SpriteRenderer aimingRenderer;
    // Start is called before the first frame update
    void Start()
    {
        aimingRenderer = GetComponent<SpriteRenderer>();
        aimingRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAiming)
        {
            aimingRenderer.enabled = true;
            RotateAiming();
        }
        else if (!player.isAiming)
        {
            aimingRenderer.enabled = false;
        }
    }

    void RotateAiming()
    {
        transform.LookAt(player.transform.position + player.xzAim);
        Vector3 rototo = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(90, rototo.y + 90, 0);

    }
}
