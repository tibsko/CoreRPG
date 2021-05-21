using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] LayerMask collisionMask;
    [SerializeField] int damages;
    [SerializeField] float damageTimeRate;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject target;

    void Start() {
        InvokeRepeating(nameof(DamagesZone), 0, damageTimeRate);
    }
    private void OnTriggerEnter(Collider other) {
        GenericHealth health = other.GetComponent<GenericHealth>();
        if (!enemies.Contains(other.gameObject) && health) {
            target = other.gameObject;
            enemies.Add(target);
        }

    }
    private void OnTriggerExit(Collider other) {
        if (enemies.Contains(other.gameObject))
            enemies.Remove(other.gameObject);
    }
    public void DamagesZone() {
        if (enemies.Count > 0) {
            Debug.Log(damages);
            foreach (GameObject target in enemies) {
                if (target) {
                    GenericHealth health = target.GetComponent<GenericHealth>();
                    if (health)
                        health.TakeDamage(damages, this.gameObject);
                }
            }
        }
    }


}
