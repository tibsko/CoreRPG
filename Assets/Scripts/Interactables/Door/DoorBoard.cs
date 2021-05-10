using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBoard : MonoBehaviour
{
    public bool IsActive { get; set; }

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start() {
        IsActive = true;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }
    private void Update() {
        ActivateBoard(IsActive);
    }
    public void ActivateBoard(bool active)
    {
        meshRenderer.enabled = active;
        //boxCollider.enabled = active;
    }
    
}
