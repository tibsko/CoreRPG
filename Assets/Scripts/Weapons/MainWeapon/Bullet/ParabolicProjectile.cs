using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParabolicProjectile : MonoBehaviour {
    public float height;

    [SerializeField] float speed;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float impactDelay=2f;
    [SerializeField] bool impactOnFloor = false;
    [SerializeField] bool impactDamage;
    [SerializeField] LayerMask collisionMask;

    //public float Damages { get ; set; }
    public bool throwObject = false;
    public bool drawGiz = true;
    public CollisionEvent onImpact;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float animationProjectile;
    private bool impactInstantiate = false;

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
        Debug2.Log("collision");
        if (collisionMask.ContainsLayer(collider.gameObject.layer)) {
                Vector3 impactPosition = transform.position;
            if(impactDamage)
                onImpact.Invoke(impactPosition);
            if (impactEffect && !impactInstantiate) {
                if (impactOnFloor) {
                    impactPosition.y = collider.transform.position.y; //TODO Improve height calculation
                }
                GameObject goEffect =Instantiate(impactEffect, impactPosition, Quaternion.identity);
                impactInstantiate = true;
                Destroy(goEffect, impactDelay);
            }
        }
    }
    [System.Serializable]
    public class CollisionEvent : UnityEvent<Vector3> { }

}
