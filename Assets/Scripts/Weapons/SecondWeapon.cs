using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = " New Second weapon", menuName = "Second Weapon")]
public class SecondWeapon : ScriptableObject {

    public Sprite icon = null;
    public bool isDefaultweapon = false;
    public int damages;
    public float range;
    public int nbShoot;
    public SecondWeaponType secondWeaponType;

    // Start is called before the first frame update

    public virtual void Use() {
        
    }
    public enum SecondWeaponType { Grenade, Claymore, trick }
}
