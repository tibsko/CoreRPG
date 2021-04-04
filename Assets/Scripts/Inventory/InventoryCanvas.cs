using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour {

    private bool isActive;
    public void Start() {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void ActivateInventory() {
        if (isActive) {
            isActive = false;
        }
        else {
            isActive = true;
        }
        gameObject.SetActive(isActive);

        
    }
}

