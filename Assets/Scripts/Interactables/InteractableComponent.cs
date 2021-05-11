using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour {
    public bool useOnce = false;
    public bool used = false;
    public bool hasInteracted = false;
    public string textButton;

    bool isFocus = false;
    Transform player;

    public InteractionEvent onInteract;
    public UnityEvent onHoldDown;
    public UnityEvent onHoldUp;
    public void Interact(GameObject player) {
        if (!useOnce || (useOnce && !used)) {
            onInteract.Invoke(player);
            used = true;
        }
    }

    public void HoldDownInteract() {
        if (!useOnce || (useOnce && !used)) {
            onHoldDown.Invoke();
            used = true;
        }
    }

    public void HoldUpInteract() {
        onHoldUp.Invoke();
    }
    void Update() {

    }
    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        HUD.instance.ActivateButton(true);
        HUD.instance.NameButton(textButton);
    }


    public void OnDeFocused() {
        isFocus = false;
        player = null;
        hasInteracted = false;
        HUD.instance.ActivateButton(false);

    }
    [System.Serializable]
    public class InteractionEvent : UnityEvent<GameObject> { }

}
