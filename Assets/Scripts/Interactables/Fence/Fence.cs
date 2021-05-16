using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {

    public int healthStep;
    [SerializeField] float timer = 0.5f;
    [SerializeField] GameObject[] planks;

    private GenericHealth doorHealth;
    private bool repairing = false;
    private float repairingTime;

    void Start() {
        doorHealth = gameObject.GetComponent<GenericHealth>();
        healthStep = (int)Mathf.Round(doorHealth.maxHealth / (planks.Length - 1));

        for (int i = 0; i < planks.Length; i++) {
            planks[i].SetActive(true);
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



        //    if (doorHealth.CurrentHealth == doorHealth.maxHealth) {
        //        nbActivePlanks = fenceInteractable.nbPlanks;
        //    }
        //    else if (CurrentHealth <= 0) {
        //        nbActivePlanks = 0;
        //    }
        //    else {
        //        nbActivePlanks = (int)Mathf.Round(CurrentHealth / fenceInteractable.healthStep + 0.7f);
        //    }

        //    for (int i = 0; i < fenceInteractable.nbPlanks; i++) {
        //        for (int j = 0; j < nbActivePlanks; j++) {
        //            fenceInteractable.planks[j].IsActive = true;
        //        }
        //        for (int k = nbActivePlanks; k < fenceInteractable.nbPlanks; k++) {
        //            fenceInteractable.planks[k].IsActive = false;
        //        }
        //    }
        //}
    }
}
