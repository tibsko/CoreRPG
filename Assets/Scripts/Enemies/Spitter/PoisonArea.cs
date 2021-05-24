using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour {

    [SerializeField] int damages = 5;
    [SerializeField] float damageFrequency = 0.5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float radius = 2f;
    [SerializeField] GameObject spriteZone;
    [SerializeField] float maxScale;

    private ParticleSystem particles;
    private float scale;


    void Start() {
        
        InvokeRepeating(nameof(DamageBurst), 0, damageFrequency);
        //spriteZone.transform.localScale = Vector3.zero;
        float rotation = Random.Range(0, 360);
        spriteZone.transform.Rotate(new Vector3(0, 0, rotation));
        //scale = 0;
        //InvokeRepeating(nameof(SpriteSize), 0f, .05f);
        Invoke(nameof(Stop), lifeTime);
    }

    

    private void DamageBurst() {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, ReferenceManager.instance.playerLayer);
        foreach (Collider collider in cols) {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth) {
                playerHealth.TakeDamage(damages, gameObject);
            }
        }
    }

    //private void SpriteSize() {
    //    if (spriteZone.transform.localScale.x < maxScale) {
    //        scale += .1f;
    //        spriteZone.transform.localScale = Vector3.one * scale;
    //    }
    //}
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
