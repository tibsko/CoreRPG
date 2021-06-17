using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour {
    public float damages = 40f;

    [SerializeField] ParticleSystem flamesParticles;
    [SerializeField] float timeBetweenBurst = 1.5f;
    [SerializeField] float flamesDuration = 4f;
    [SerializeField] int burstNumber = 3;

    private Collider flamesColider;
    private bool active = false;
    private int burstCount = 0;
    private const float colliderAtivationTime = .5f;

    void Start() {
        flamesParticles.Stop();
        flamesColider = flamesParticles.GetComponent<Collider>();
        flamesColider.enabled = false;
    }

    public void Enable() {
        if (!active) {
            active = true;
            burstCount = 0;
            StartCoroutine(FlamesBurt());
        }
    }

    public void Disable() {
        active = false;
        flamesColider.enabled = false;
    }

    public void DealDamages(GameObject gameObject) {
        if (!active)
            return;

        if (gameObject.HasComponent(out GenericHealth health)) {
            health.TakeDamage(damages, this.gameObject);
        }
    }

    private IEnumerator FlamesBurt() {
        if (burstCount < burstNumber && active) {
            burstCount++;

            yield return new WaitForSeconds(timeBetweenBurst);
            flamesParticles.Play();
            yield return new WaitForSeconds(colliderAtivationTime);
            flamesColider.enabled = true;
            yield return new WaitForSeconds(flamesDuration - colliderAtivationTime);
            flamesParticles.Stop();
            flamesColider.enabled = false;

            StartCoroutine(FlamesBurt());
        }
        else {
            active = false;
            flamesColider.enabled = false;
        }
    }



}
