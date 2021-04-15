using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerActivator : MonoBehaviour {
    [SerializeField] MonoBehaviour collectable;
    [SerializeField] float timer;

    void Start() {
        StartCoroutine(ActivateComponent());
    }

    private IEnumerator ActivateComponent() {
        yield return new WaitForSeconds(timer);
        collectable.enabled = true;
    }
}
