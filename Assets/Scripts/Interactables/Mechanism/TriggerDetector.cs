using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour {

    public UnityEventGO onDetectEnter;
    public UnityEventGO onDetectStay;
    public UnityEventGO onDetectExit;

    private void OnTriggerEnter(Collider other) {
        onDetectEnter.Invoke(other.gameObject);
    }

    private void OnTriggerStay(Collider other) {
        onDetectStay.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        onDetectExit.Invoke(other.gameObject);
    }
}

[System.Serializable]
public class UnityEventGO : UnityEvent<GameObject> { }