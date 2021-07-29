using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] float radiusInteractable = 3.5f;
    [SerializeField] LayerMask interacableLayers;

    private InteractableComponent focus;
    private List<InteractableComponent> interactables = new List<InteractableComponent>();

    private void OnTriggerStay(Collider other) {
        if (interacableLayers.ContainsLayer(other.gameObject.layer)) {
            InteractableComponent interactable = other.GetComponent<InteractableComponent>();
            interactables.Add(interactable);
            SetFocus();
        }
    }
    private void OnTriggerExit(Collider other) {
        if (focus) {
            if (other.gameObject == focus.gameObject) {
                interactables.Remove(other.GetComponent<InteractableComponent>());
                RemoveFocus();
            }
        }
    }


    private void SetFocus() {
        float distanceMin = 100f;
        if (interactables.Count > 0) {
            foreach (InteractableComponent inter in interactables) {
                float distance = Vector3.Distance(transform.position, inter.gameObject.transform.position);
                if (distance < distanceMin) {
                    distanceMin = distance;
                    focus = inter;
                }
            }
            focus.OnFocused(transform);
        }
        else { RemoveFocus(); }
    }

    public void RemoveFocus() {
        if (focus != null)
            focus.OnDeFocused();
        focus = null;
        interactables = new List<InteractableComponent>();
    }


    public void Interaction() {
        if (focus) {
            focus.Interact(gameObject);
        }

        RemoveFocus();
    }
    public void OnInteraction(InputAction.CallbackContext context) {
        if (focus) {
            if (!focus.needPress && context.phase == InputActionPhase.Performed) {
                Interaction();
                Debug2.Log("coucou");
            }

        }
    }
    public void OnHoldInteraction(InputAction.CallbackContext context) {
        if (focus && focus.needPress&&context.phase == InputActionPhase.Performed) {
            HoldDownInteraction();
        }
    }
    public void OnUpInteraction(InputAction.CallbackContext context) {
        if (focus && focus.needPress&& context.phase == InputActionPhase.Performed) {
            HoldupInteraction();
        }
    }

    public void HoldDownInteraction() {
        focus.HoldDownInteract();
    }
    public void HoldupInteraction() {
        focus.HoldUpInteract();
    }
}
