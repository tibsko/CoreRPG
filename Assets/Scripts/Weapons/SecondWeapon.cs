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

    [SerializeField] GameObject bullet;

    [HideInInspector] public UnityEvent onEndAttack;

    // Start is called before the first frame update

    public virtual void Use() {
        Debug.Log("use");
    }

    

    public void Attack() {
        Transform player = PlayerManager.instance.GetNearestPlayer(transform.position).transform;
        Instantiate(bullet,player);
    }

    public bool CanAttack() {
        return true;

    }
    public enum SecondWeaponType { Grenade, Claymore, trick }
}
