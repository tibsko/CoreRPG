using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float exploseRadius;
    [SerializeField] int damages;
    public void Explosion(Vector3 impactPosition) {
        Collider[] hitColliders = Physics.OverlapSphere(impactPosition, exploseRadius, collisionMask);
        foreach (Collider col in hitColliders) {
            GenericHealth health = col.gameObject.GetComponent<GenericHealth>();
            if (health)
                health.TakeDamage(damages, this.gameObject);
        }
        Destroy(gameObject, .1f);

    }
}
