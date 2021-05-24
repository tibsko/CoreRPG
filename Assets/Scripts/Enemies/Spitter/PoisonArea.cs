using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour {

    [SerializeField] float lifeTime = 5f;
    [SerializeField] float radius = 2f;
    [SerializeField] GameObject spriteZone;

    private ParticleSystem particles;
    private float scale;


    void Start() {
        
        float rotation = Random.Range(0, 360);
        spriteZone.transform.Rotate(new Vector3(0, 0, rotation));
        Invoke(nameof(Stop), lifeTime);
    }

    /// <summary>
    /// Script Obselete
    /// </summary>

    private void DamageBurst() {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, ReferenceManager.instance.playerLayer);
        foreach (Collider collider in cols) {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth) {
                Vector3 poisonPosition = collider.transform.position + new Vector3(0, 1f, 0);
                
            }
        }
    }

   
    private void Stop() {
        if(particles)
            particles.Stop();
        this.enabled = false;
        Destroy(gameObject,1);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
