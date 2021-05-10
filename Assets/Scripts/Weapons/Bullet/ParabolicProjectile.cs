using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float height;
    [SerializeField] GameObject impactEffect;
    [SerializeField] bool impactOnFloor = false;
    [SerializeField] bool dealDamages = false;
    [SerializeField] LayerMask collisionMask;

    public float Damages { get; set; }

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float animationProjectile;

    private void Start() {
        startPosition = transform.position;
    }

    void Update() {
        animationProjectile += Time.deltaTime;
        //animationProjectile = animationProjectile % speed;
        transform.position = ParabolaEquation.Parabole(startPosition, endPosition, height, animationProjectile / speed);
    }

    public void SetTarget(Vector3 target) {
        endPosition = target;
    }

    void OnTriggerEnter(Collider collider) {

        if (collisionMask.ContainsLayer(collider.gameObject.layer)) {
            if (impactEffect) {
                Vector3 impactPosition = collider.gameObject.transform.position;
                if (impactOnFloor) {
                    impactPosition.y = collider.transform.position.y;
                }
                Instantiate(impactEffect, impactPosition, Quaternion.identity);
                Destroy(gameObject);
            }

            GenericHealth genericHealth = collider.GetComponent<GenericHealth>();
            if (genericHealth && dealDamages) {
                genericHealth.TakeDamage(Damages, this.gameObject);
            }
        }
    }
}
