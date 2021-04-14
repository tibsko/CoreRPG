using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableCheck : MonoBehaviour {
    
    [SerializeField] SphereCollider collactableCheck;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider collider) {
        CollactableItem collactable = collider.GetComponent<CollactableItem>();
        if (collactable) {
            collactable.Use(gameObject);
            Debug.Log("collactable detected");

        }
    }

}
