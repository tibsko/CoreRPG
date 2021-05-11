using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : MonoBehaviour {
    public float height;

    [SerializeField] float speed;
    [SerializeField] GameObject impactEffect;
    [SerializeField] bool impactOnFloor = false;
    [SerializeField] bool dealDamages = false;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] bool exploseProjetcile;
    [SerializeField] float exploseRadius;
    [SerializeField] float damages;

    //public float Damages { get ; set; }
    public bool throwObject = false;
    public bool drawGiz = true;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float animationProjectile;

    private void Start() {
        startPosition = transform.position;

    }

    void Update() {
        if (throwObject) {
            animationProjectile += Time.deltaTime;
            //animationProjectile = animationProjectile % speed;
            transform.position = ParabolaEquation.Parabole(startPosition, endPosition, height, animationProjectile / speed);
        }
    }

    public void SetTarget(Vector3 target) {
        endPosition = target;
    }

    void OnCollisionEnter(Collision collider) {

        if (collisionMask.ContainsLayer(collider.gameObject.layer)) {
                Vector3 impactPosition = transform.position;
            if (impactEffect) {
                if (impactOnFloor) {
                    impactPosition.y = collider.transform.position.y; //TODO Improve height calculation
                }
                GameObject goEffect =Instantiate(impactEffect, impactPosition, Quaternion.identity);
                Destroy(goEffect, 2f);
            }
            if (exploseProjetcile) {
                Collider[] hitColliders = Physics.OverlapSphere(impactPosition, exploseRadius,collisionMask);
                foreach(Collider col in hitColliders) {
                    GenericHealth health = col.gameObject.GetComponent<GenericHealth>();
                    if(health&&dealDamages)
                        //Debug.Log( col.gameObject.name + Damages);
                        health.TakeDamage(damages, this.gameObject);
                }
                Destroy(gameObject,.1f);
            }

            GenericHealth genericHealth = collider.gameObject.GetComponent<GenericHealth>();
            if (genericHealth && dealDamages) {
                genericHealth.TakeDamage(damages, this.gameObject);
                Destroy(gameObject);

            }
        }
    }

}
