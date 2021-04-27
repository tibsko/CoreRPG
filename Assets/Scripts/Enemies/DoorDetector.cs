using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetector : MonoBehaviour {
    public Transform doorTransform;
    void Start() {
        doorTransform =  PlayerManager.instance.player.transform;

    }
    void OnTriggerStay(Collider colliders) {
        DoorHealth door = colliders.GetComponent<DoorHealth>();
        if (door) {
            doorTransform = door.gameObject.transform;
        }
    }
}
