using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSpit : MonoBehaviour
{
    [SerializeField] GameObject spitBullet;
    [SerializeField] Transform launcherTransform;
    void Start() {
        spitBullet.GetComponent<ParabolicProjectile>().startPosition = launcherTransform;
    }
    public void SpitAttack() {
        Instantiate(spitBullet, launcherTransform);
    }
}
