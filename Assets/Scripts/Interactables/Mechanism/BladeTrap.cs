using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour {
    public float damages = 40f;

    [SerializeField] Transform blade;
    [SerializeField] int slashNumber = 6;
    [SerializeField] float rangeHalfAngle = 90f;
    [SerializeField] float angularSpeed = 0.5f;
    [SerializeField] float timeBetweenSlash = 0.5f;

    private Quaternion desiredRot;
    private bool active = false;
    private float waitTimer = 0;
    private int slashCount = 0;

    void Start() {
        blade.localRotation = Quaternion.Euler(rangeHalfAngle, 0, 0);
        desiredRot = Quaternion.AngleAxis(-rangeHalfAngle, blade.right);
    }

    private void Update() {
        if (!active)
            return;

        if (slashCount < slashNumber) {
            if (waitTimer > 0) {
                waitTimer -= Time.deltaTime;
            }
            else {
                Quaternion rot = Quaternion.Slerp(blade.localRotation, desiredRot, Time.deltaTime * angularSpeed);
                blade.localRotation = rot;
                Debug2.Log(rot);

                float angle = Quaternion.Angle(blade.localRotation, desiredRot);
                if (angle <= 0.1f) {
                    waitTimer = timeBetweenSlash;
                    desiredRot = Quaternion.Inverse(desiredRot);
                    slashCount++;
                    Debug2.Log("Inversing rotation");
                }
            }
        }
        else
            Disable();
    }

    public void DealDamages(GameObject gameObject) {
        if (!active || waitTimer > 0)
            return;

        if (gameObject.HasComponent(out GenericHealth health)) {
            health.TakeDamage(damages, this.gameObject);
        }
    }

    public void Enable() {
        if (!active) {
            active = true;
            slashCount = 0;
        }
    }

    public void Disable() {
        active = false;
    }

}
