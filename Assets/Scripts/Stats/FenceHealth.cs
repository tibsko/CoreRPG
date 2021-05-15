using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FenceHealth : GenericHealth {

    [SerializeField] Fence fenceInteractable;
    private int activeDoorCount;

    //private NavMeshObstacle obstacle;
    public override void Start() {
        base.Start();
        fenceInteractable = GetComponent<Fence>();
    }
    // Update is called once per frame

    public void UpdateFenceBoards() {
        if (CurrentHealth == maxHealth) {
            activeDoorCount = fenceInteractable.nbFence;
        }
        else if (CurrentHealth <= 0) {
            activeDoorCount = 0;
        }
        else {
            activeDoorCount = (int)Mathf.Round(CurrentHealth / fenceInteractable.healthStep + 0.7f);
        }

        for (int i = 0; i < fenceInteractable.nbFence; i++) {
            for (int j = 0; j < activeDoorCount; j++) {
                fenceInteractable.planks[j].IsActive = true;
            }
            for (int k = activeDoorCount; k < fenceInteractable.nbFence; k++) {
                fenceInteractable.planks[k].IsActive = false;
            }
        }
    }

}


