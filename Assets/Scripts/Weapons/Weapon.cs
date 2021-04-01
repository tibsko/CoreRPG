using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public float autoshootDistance;
    public int combosCount = 1;

    [SerializeField] protected WeaponData weaponData;
    public int Damages { get; private set; }
    public float Cooldown { get; private set; }


    // Start is called before the first frame update
    protected void Start() {
        Damages = weaponData.damages;
        Cooldown = weaponData.cooldown;
    }

    public AnimatorOverrideController GetAnimatorOverride() {
        return weaponData.overrideAnimator;
    }

    public abstract void Attack();

    public abstract bool CanAttack();


}



