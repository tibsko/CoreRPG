﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] LayerMask collisionMask;
    [SerializeField] int damages;
    [SerializeField] float damageTimeRate;
    [SerializeField] float damageTime;
    [SerializeField] GameObject effectOnPlayer;

    private List<GameObject> enemies = new List<GameObject>();
    private GameObject target;
    void Start() {
        InvokeRepeating(nameof(DamagesZone), 0, damageTimeRate);

    }
    private void OnTriggerEnter(Collider other) {
        GenericHealth health = other.GetComponent<GenericHealth>();
        if (!enemies.Contains(other.gameObject) && health && collisionMask.ContainsLayer(other.gameObject.layer)) {
            target = other.gameObject;
            enemies.Add(target);

        }

    }
    private void OnTriggerExit(Collider other) {
        if (enemies.Contains(other.gameObject)) {
            if (other.HasComponent(out GenericHealth health)) {
                health.TakeDamageInTime(damages, damageTimeRate, damageTime, this.gameObject);
                if (effectOnPlayer) {
                    Vector3 poisonPosition = target.transform.position + new Vector3(0, 1f, 0);
                    GameObject par = Instantiate(effectOnPlayer, poisonPosition, Quaternion.identity, health.transform);
                    Destroy(par, damageTime);
                }
            }
            enemies.Remove(other.gameObject);
        }

    }
    public void DamagesZone() {
        if (enemies.Count > 0) {
            foreach (GameObject target in enemies) {
                if (target) {
                    GenericHealth health = target.GetComponent<GenericHealth>();
                    if (health) {
                        health.TakeDamage(damages, this.gameObject);
                        if (effectOnPlayer) {
                            Vector3 poisonPosition = target.transform.position + new Vector3(0, 1f, 0);
                            GameObject par = Instantiate(effectOnPlayer, poisonPosition, Quaternion.identity, health.transform);
                            Destroy(par, damageTimeRate);
                        }
                    }
                }
            }
        }
    }


}
