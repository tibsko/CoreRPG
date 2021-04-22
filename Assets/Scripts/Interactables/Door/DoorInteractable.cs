using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public DoorBoard[] doorBoards;

    private DoorHealth doorHealth;
    private bool repair = false;
    private float currentTime;

    [SerializeField] float timer = 0.5f;
    

    void Start() {
        doorBoards = gameObject.GetComponentsInChildren<DoorBoard>();
        doorHealth = gameObject.GetComponent<DoorHealth>();
        for (int i = 0; i < doorBoards.Length; i++) {
            doorBoards[i].isActive = true;
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

    public override void HoldDownInteract() {
        base.HoldDownInteract();
        repair = true;
    }
    public override void HoldUpInteract() {
        base.HoldUpInteract();
        repair = false;
    }

    private void RepairDoor() {
        if (doorHealth.currentHealth < doorHealth.maxHealth) {
            doorHealth.HealHealth(20);
            Debug.Log("repairing");
        }
    }
}
