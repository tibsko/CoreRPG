using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//[CreateAssetMenu(fileName = " New Second weapon", menuName = "Second Weapon")]
public class SecondWeapon : MonoBehaviour {

    public Sprite icon = null;
    public bool isDefaultweapon = false;
    public int damages;
    public float range;
    
    public SecondWeaponType secondWeaponType;


    [HideInInspector] public UnityEvent onEndAttack;

    // Start is called before the first frame update

    public virtual void Use() {
        Debug.Log("use");
    }

    public virtual void Attack() {
        
    }

    public bool CanAttack() {
        return true;

    }
    public virtual void OnAim(Vector2 aim) {

    }
    public enum SecondWeaponType { Grenade, Claymore, Consumable}
}
