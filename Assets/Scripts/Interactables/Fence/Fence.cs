using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fence : MonoBehaviour {

    [SerializeField] float timer = 0.5f;
    [SerializeField] Transform planksParent;

    private List<Transform> planks;
    private GenericHealth doorHealth;
    private bool repairing = false;
    private float repairingTime;
    private float lifePointsPerPlank;

    void Start() {
        planks = planksParent.GetComponentsInChildren<Transform>().ToList();
        planks.Remove(planksParent);
        doorHealth = gameObject.GetComponent<GenericHealth>();
        lifePointsPerPlank = (float)doorHealth.maxHealth / planks.Count;

        for (int i = 0; i < planks.Count; i++) {
            planks[i].gameObject.SetActive(true);
        }
    }

    void Update() {
        if (repairing) {
            repairingTime += Time.fixedDeltaTime;  // ajoute a chaque update le temps écoulé depuis le dernier Update		
            if (repairingTime > timer) {
                RepairDoor();
                repairingTime = 0;
            }
        }
    }

    public void HoldDownInteract() {
        repairing = true;
        Debug.Log("Down");
    }
    public void HoldUpInteract() {
        repairing = false;
        Debug.Log("Up");

    }

    private void RepairDoor() {
        if (doorHealth.CurrentHealth < doorHealth.maxHealth) {
            doorHealth.Heal(20, gameObject);
            UpdateFencePlanks();
            Debug.Log("Repairing");
        }
        else {
            repairing = false;
        }
    }

    public void UpdateFencePlanks() {

        for (int i = 0; i < planks.Count; i++) {
            bool showPlank = doorHealth.CurrentHealth >= i * lifePointsPerPlank + 0.1f;
            planks[i].gameObject.SetActive(showPlank);
        }
    }
}