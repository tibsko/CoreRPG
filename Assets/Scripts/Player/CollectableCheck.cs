using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCheck : MonoBehaviour {
    
    [SerializeField] CapsuleCollider collectableCheck;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerStay(Collider collider) {
        CollectableItem collectable = collider.GetComponent<CollectableItem>();
        if (collectable && collectable.enabled) {
            collectable.Use(gameObject);
            Debug.Log("Collectable detected");

        }
    }

}
