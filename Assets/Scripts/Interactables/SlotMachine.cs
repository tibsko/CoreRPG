using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [System.Serializable]
    public class WeaponBuy {
        public GameObject weapon;
        public float rate;
    }
    [SerializeField] List<WeaponBuy> weaponsBuy;
    [SerializeField] float speedRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WeaponRandom() {
        float rand = Random.Range(0f, 100f);
        float total = 0;
        for (int i = 0; i < weaponsBuy.Count; i++) {
            total += weaponsBuy[i].rate;
            if (rand < total) {
                Instantiate(weaponsBuy[i].weapon,
                    transform.position + new Vector3(Random.Range(0f, 1f),
                    0,
                    Random.Range(0f, 1f)),
                    Quaternion.identity);
                break;
            }
        }

    }
}
