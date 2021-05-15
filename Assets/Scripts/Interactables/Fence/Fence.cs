using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    public int healthStep;

    public int nbFence;
    public FenceBoard[] planks;

    private FenceHealth doorHealth;
    private bool repair = false;
    private float currentTime;

    [SerializeField] float timer = 0.5f;

    void Start() {
        planks = gameObject.GetComponentsInChildren<FenceBoard>();
        nbFence = planks.Length;
        doorHealth = gameObject.GetComponent<FenceHealth>();
        healthStep = (int)Mathf.Round(doorHealth.maxHealth / (nbFence-1));

        for (int i = 0; i < nbFence; i++) {
            planks[i].IsActive = true;
        }

    }
    void Update() {
        if (repair) {
            currentTime += Time.fixedDeltaTime;  // ajoute a chaque update le temps écoulé depuis le dernier Update		
            if (currentTime > timer) {
                RepairDoor();
                currentTime = 0;
            }
        }
    }
    
    public void HoldDownInteract() {
        repair = true;
        Debug.Log("Down");
    }
    public void HoldUpInteract() {
        repair = false;
        Debug.Log("Up");

    }

    private void RepairDoor() {
        if (doorHealth.CurrentHealth < doorHealth.maxHealth) {
            doorHealth.Heal(20,gameObject);
            doorHealth.UpdateFenceBoards();
            Debug.Log("repairing");
        }
        else {
            repair = false;
        }
    }
}
