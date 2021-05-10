using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerActivator : MonoBehaviour {

    [SerializeField] MonoBehaviour script;
    [SerializeField] float timer;

    void Start() {
        script.enabled = false;
        StartCoroutine(ActivateComponent());
    }

    private IEnumerator ActivateComponent() {
        yield return new WaitForSeconds(timer);
        script.enabled = true;
    }
}
