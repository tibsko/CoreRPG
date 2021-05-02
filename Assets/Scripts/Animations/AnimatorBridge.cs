using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBridge : MonoBehaviour {

    private Animator animator;

    private Dictionary<string, AnimLock> locksDictonary;


    private void Start() {
        animator = GetComponent<Animator>();
        if (!animator) {
            Debug.LogError("Can't find animator on gameobject");
        }
        locksDictonary = new Dictionary<string, AnimLock>();
    }

    private void Update() {
        List<string> keys = new List<string>(this.locksDictonary.Keys); ;
        for (int i = 0; i < keys.Count; i++) {
            if (locksDictonary[keys[i]].cooldown > 0) {
                AnimLock animLock = locksDictonary[keys[i]];
                animLock.cooldown -= Time.deltaTime;
                locksDictonary[keys[i]] = animLock;
            }
        }
    }

    public void SetLock(string name, bool state) {
        AnimLock animLock;
        animLock.name = name;
        if (locksDictonary.TryGetValue(name, out animLock)) {
            animLock.lockAnimation = state;
            locksDictonary[name] = animLock;
        }
        else {
            animLock.lockAnimation = state;
            animLock.cooldown = 0;
            locksDictonary.Add(name, animLock);
        }
    }

    public void AddCooldown(string name, float cooldown) {
        AnimLock animLock;
        if (locksDictonary.TryGetValue(name, out animLock)) {
            animLock.cooldown = cooldown;
            locksDictonary[name] = animLock;
        }
        else {
            animLock.lockAnimation = false;
            animLock.cooldown = cooldown;
            locksDictonary.Add(name, animLock);

        }
    }

    public bool SetBool(string name, bool value, float delay = 0) {
        if (delay > 0)
            StartCoroutine(SetBoolDelay(name, value, delay));
        else
            animator.SetBool(name, value);
        return true;
    }

    //////////////////Override animator methods
    public void CancelTrigger(string name) {
        animator.ResetTrigger(name);
    }


    public bool SetInteger(string name, int value, float delay = 0) {
        if (delay > 0)
            StartCoroutine(SetIntegerDelay(name, value, delay));
        else
            animator.SetInteger(name, value);
        return true;
    }

    public bool SetFloat(string name, float value, float dampTime = 0, float deltaTime = 0, float delay = 0) {
        if (delay > 0)
            StartCoroutine(SetFloatDelay(name, value, dampTime, deltaTime, delay));
        else if (dampTime > 0 && deltaTime > 0)
            animator.SetFloat(name, value, dampTime, deltaTime);
        else
            animator.SetFloat(name, value);
        return true;
    }

    public bool SetTrigger(string name, float delay = 0) {
        AnimLock animLock;
        
        if (locksDictonary.TryGetValue(name, out animLock)) {
            if (animLock.lockAnimation || animLock.cooldown > 0)
                return false;
        }

        if (delay > 0)
            StartCoroutine(SetTriggerDelay(name, delay));
        else
            animator.SetTrigger(name);

        return true;
    }

    private IEnumerator SetBoolDelay(string name, bool value, float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetBool(name, value);
    }

    private IEnumerator SetIntegerDelay(string name, int value, float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetInteger(name, value);
    }

    private IEnumerator SetFloatDelay(string name, float value, float dampTime, float deltaTime, float delay) {
        yield return new WaitForSeconds(delay);
        if (dampTime > 0 && deltaTime > 0)
            animator.SetFloat(name, value, dampTime, deltaTime);
        else
            animator.SetFloat(name, value);

    }

    private IEnumerator SetTriggerDelay(string name, float delay) {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(name);
    }
}

[System.Serializable]
public struct AnimLock {
    public string name;
    public bool lockAnimation;
    public float cooldown;
}