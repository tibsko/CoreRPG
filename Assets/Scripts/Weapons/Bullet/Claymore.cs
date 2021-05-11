using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claymore : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    [SerializeField] bool dealDamages = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float exploseRadius;
    [SerializeField] float damages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        if (layerMask.ContainsLayer(other.gameObject.layer)) {
            Debug.Log("Detect");
            if (impactEffect) {
                GameObject goEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
                Destroy(goEffect, 2f);
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, exploseRadius, layerMask);
                foreach (Collider col in hitColliders) {
                    GenericHealth health = col.gameObject.GetComponent<GenericHealth>();
                    if (health&&dealDamages)
                        //Debug.Log( col.gameObject.name + Damages);
                        health.TakeDamage(damages, this.gameObject);
                }
                Destroy(gameObject, .1f);
            }
        }
    }
}
