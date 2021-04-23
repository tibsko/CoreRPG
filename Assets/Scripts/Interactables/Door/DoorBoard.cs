using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBoard : MonoBehaviour
{
    public bool isActive;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    void Start() {
        isActive = true;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }
    private void Update() {
        ActivateBoard(isActive);
    }
    public void ActivateBoard(bool active)
    {
        meshRenderer.enabled = active;
        //boxCollider.enabled = active;
    }
    
}
