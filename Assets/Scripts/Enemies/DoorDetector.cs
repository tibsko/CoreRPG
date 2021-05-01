﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetector : MonoBehaviour {

    public Transform doorDetected;
    //void Awake() {
    //    doorDetected = PlayerManager.instance.player.transform;
    //}
    void Start() {
        doorDetected = PlayerManager.instance.player.transform;
    }
    void OnTriggerStay(Collider colliders) {
        DoorHealth door = colliders.GetComponent<DoorHealth>();
        if (door) {
            doorDetected = door.gameObject.transform;
        }
    }
}