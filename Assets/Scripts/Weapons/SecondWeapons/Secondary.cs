using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//[CreateAssetMenu(fileName = " New Second weapon", menuName = "Second Weapon")]
public class Secondary : MonoBehaviour {

    public Sprite icon = null;
    public bool isDefaultweapon = false;
    public float range;
    public bool display;


    public SecondaryType secondaryType;


    [HideInInspector] public UnityEvent onEndAttack;

    // Start is called before the first frame update

    public virtual void Use() {
        Debug2.Log("use");
    }

    public virtual void Attack() {
        
    }

    public bool CanAttack() {
        return true;

    }
    public virtual void OnAim(Vector2 aim) {

    }

    public virtual void OnRelease(Vector2 aim) {
        //Attack();
    }

    public enum SecondaryType { Grenade, Claymore, Consumable,Tower}
}
