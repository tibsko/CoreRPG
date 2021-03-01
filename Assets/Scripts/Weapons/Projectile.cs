using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float actualDistance;
    private float distance;
    private Vector3 targetPos;

    private void Start() {

    }

    //public void Init(Vector3 target) {

    //    distance = target - transform.position;
    //    actualDistance = distance;
    //}


    //private void Update() {
    //    float f = actualDistance / distance;
    //    Vector3 lookPoint = new Vector3(targetPos.x, difference.magnitude * (1 - f), targetPos.z);
    //    transform.LookAt(lookPoint);
    //    MoveMissile();

    //    actualDistance = transform.position - sourcePos;
    //    if (distanceFromSource.magnitude >= Range) {
    //        Destroy(gameObject);
    //        return;
    //    }
    //}

    //private void MoveMissile() {
    //    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    //}
}
