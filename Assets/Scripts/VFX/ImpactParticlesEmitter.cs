using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticlesEmitter : MonoBehaviour {

    public GameObject particlePrefab;
    public float lifeTime;

    public void InstantiateParticule(Vector3 position, Vector3 normal) {

        GameObject particles = Instantiate(particlePrefab, position, Quaternion.identity);
        particles.transform.rotation = Quaternion.Euler(normal);
        Destroy(particles, lifeTime);
    }
}
