using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claymore : MonoBehaviour {
    public bool canExplose;

    [SerializeField] GameObject impactEffect;
    [SerializeField] bool dealDamages = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float exploseRadius;
    [SerializeField] float damages;

    void OnTriggerEnter(Collider other) {
        if (canExplose) {
            if (layerMask.ContainsLayer(other.gameObject.layer)) {
                Debug.Log("Detect");
                if (impactEffect) {
                    GameObject goEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
                    Destroy(goEffect, 2f);
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, exploseRadius, layerMask);
                    foreach (Collider col in hitColliders) {
                        GenericHealth health = col.gameObject.GetComponent<GenericHealth>();
                        if (health && dealDamages)
                            //Debug.Log( col.gameObject.name + Damages);
                            health.TakeDamage(damages, this.gameObject);
                    }
                    Destroy(gameObject, .1f);
                }
            }
        }
    }
}
