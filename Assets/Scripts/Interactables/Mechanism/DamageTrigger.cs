using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageTrigger : MonoBehaviour
{
    public GOEvent onHit;

    private void OnTriggerEnter(Collider other) {
        onHit.Invoke(other.gameObject);
    }
}

[System.Serializable]
public class GOEvent : UnityEvent<GameObject> {

}