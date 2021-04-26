using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammeThrower : FireWeapon
{

    private FlammeThrowerData flammeThrowerData;


    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        if (this.FireWeaponData.GetType() == typeof(FlammeThrowerData))
            flammeThrowerData = FireWeaponData as FlammeThrowerData;
        else {
            Debug.LogError("Wrong WeaponData Type in " + this.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool CanAttack() {
        return true;
    }
    public override void Attack() {
        GameObject go=Instantiate(bulletPrefab, firePoint);
        FlammeBullet flammebullet = go.GetComponent<FlammeBullet>();
        flammebullet.flammeThrowerData = flammeThrowerData;
        Destroy(go, flammeThrowerData.cooldown);
    }
}
