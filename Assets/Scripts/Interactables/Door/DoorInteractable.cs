using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{

    //public List<DoorBoard> activeDoorBoards;
    //public List<DoorBoard> unactiveDoorBoards;
    public int nbDoor;
    public int healthStep;


    public DoorBoard[] doorBoards;

    private DoorHealth doorHealth;
    private bool repair = false;
    private float currentTime;

    [SerializeField] float timer = 0.5f;
    

    void Start() {
        doorBoards = gameObject.GetComponentsInChildren<DoorBoard>();
        nbDoor = doorBoards.Length;
        doorHealth = gameObject.GetComponent<DoorHealth>();
        healthStep = (int)Mathf.Round(doorHealth.maxHealth / nbDoor);

        for (int i = 0; i < nbDoor; i++) {
            doorBoards[i].isActive = true;
        }

        //foreach(DoorBoard door in doorBoards) {
        //    activeDoorBoards.Add(door);
        //}
        //unactiveDoorBoards = new List<DoorBoard>();
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
