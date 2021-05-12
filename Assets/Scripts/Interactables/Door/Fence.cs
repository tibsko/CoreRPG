using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
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
        healthStep = (int)Mathf.Round(doorHealth.maxHealth / (nbDoor-1));

        for (int i = 0; i < nbDoor; i++) {
            doorBoards[i].IsActive = true;
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
        if (doorHealth.currentHealth < doorHealth.maxHealth) {
            doorHealth.Heal(20,gameObject);
            doorHealth.UpdateDoorboards();
            Debug.Log("repairing");
        }
        else {
            repair = false;
        }
    }
}
