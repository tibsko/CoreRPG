using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCheck : MonoBehaviour {
    

    void Start()
    {

    }

    void OnTriggerStay(Collider collider) {
        CollectableItem collectable = collider.GetComponent<CollectableItem>();
        if (collectable && collectable.enabled) {
            collectable.Use(gameObject);
        }
    }

}
