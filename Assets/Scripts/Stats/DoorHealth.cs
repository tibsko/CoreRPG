using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : CharacterHealth {
    [SerializeField] DoorInteractable doorInteractable;

    //void Start() {
    //    healthBar = GetComponentInChildren<HealthBar>();
    //}
    // Update is called once per frame
    void Update() {
        if (currentHealth < maxHealth * 0.75f && currentHealth > maxHealth * 0.5f) {
            doorInteractable.doorBoards[0].isActive = false;
            doorInteractable.doorBoards[1].isActive = true;
            doorInteractable.doorBoards[2].isActive = true;
            doorInteractable.doorBoards[3].isActive = true;

        }
        else if (currentHealth < maxHealth * 0.5f && currentHealth > maxHealth * 0.25f) {
            doorInteractable.doorBoards[0].isActive = false;
            doorInteractable.doorBoards[1].isActive = false;
            doorInteractable.doorBoards[2].isActive = true;
            doorInteractable.doorBoards[3].isActive = true;
        }
        else if (currentHealth < maxHealth * 0.25f && currentHealth > 0f) {
            doorInteractable.doorBoards[0].isActive = false;
            doorInteractable.doorBoards[1].isActive = false;
            doorInteractable.doorBoards[2].isActive = false;
            doorInteractable.doorBoards[3].isActive = true;

        }
        else if (currentHealth <= 0) {
            doorInteractable.doorBoards[0].isActive = false;
            doorInteractable.doorBoards[1].isActive = false;
            doorInteractable.doorBoards[2].isActive = false;
            doorInteractable.doorBoards[3].isActive = false;
            currentHealth = 0;
        }
        else {
            doorInteractable.doorBoards[0].isActive = true;
            doorInteractable.doorBoards[1].isActive = true;
            doorInteractable.doorBoards[2].isActive = true;
            doorInteractable.doorBoards[3].isActive = true;
        }
    }
}

