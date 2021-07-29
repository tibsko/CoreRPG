using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour {
    public bool useOnce = false;
    public string textButton;

    public bool hasInteracted;
    public bool needPress;
    private bool isFocused = false;
    private Transform player;

    public InteractionEvent onInteract;
    public UnityEvent onHoldDown;
    public UnityEvent onHoldUp;
    public void Interact(GameObject player) {
        if (!useOnce || (useOnce && !hasInteracted)) {
            onInteract.Invoke(player);
            hasInteracted = true;
        }
        else {
            return;
        }
    }

    public void HoldDownInteract() {
        if (!useOnce) {
            onHoldDown.Invoke();
        }
    }

    public void HoldUpInteract() {
        onHoldUp.Invoke();
    }

    public void OnFocused(Transform playerTransform) {
        isFocused = true;
        player = playerTransform;
        HUD.instance.ActivateButton(true);
        HUD.instance.NameButton(textButton);
    }


    public void OnDeFocused() {
        isFocused = false;
        player = null;
        //hasInteracted = false;
        HUD.instance.ActivateButton(false);

    }
    [System.Serializable]
    public class InteractionEvent : UnityEvent<GameObject> { }

}
