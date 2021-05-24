using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour {
    public float damages = 10;
    public float lifeTime = 5f;

    private bool active = false;

    // Start is called before the first frame update
    void Start() {

    }

   public void Enable() {
        active = true;
    }

    public void Disable() {
        active = false;
    }

    private void OnTriggerStay(Collider other) {
        if (!active)
            return;

        if (other.HasComponent(out GenericHealth health)) {
            health.TakeDamage(damages, this.gameObject);
        }
    }
}
