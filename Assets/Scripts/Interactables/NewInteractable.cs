using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewInteractable : Interactable {
    public bool useOnce = false;
    public bool used = false;

    public InteractionEvent onInteract;
    public override void Interact(GameObject player) {
        if (!useOnce || (useOnce && !used)) {
            onInteract.Invoke(player);
            used = true;
        }
    }

    [System.Serializable]
    public class InteractionEvent : UnityEvent<GameObject> { }

}
