using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DelayExtensions {

    public static void InvokeDelay(this MonoBehaviour mono, Action method, float delay) {
        mono.StartCoroutine(InvokeDelayCoroutine(method, delay));
    }

    private static IEnumerator InvokeDelayCoroutine(Action method, float delay) {
        yield return new WaitForSeconds(delay);
        method();
    }
}
