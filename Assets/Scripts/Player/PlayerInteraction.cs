using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float radiusInteractable = 3.5f;

    private Interactable focus;
    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable() {
        Collider[] interactablesDetected = Physics.OverlapSphere(transform.position, radiusInteractable, LayerManager.instance.interactableLayer);
        if (interactablesDetected.Length > 0) {
            foreach (var collider in interactablesDetected) {
                Interactable interactable = interactablesDetected[0].GetComponent<Interactable>();
                SetFocus(interactable);
            }
        }
        else
            RemoveFocus();
    }

    private void SetFocus(Interactable newFocus) {
        if (newFocus && newFocus != focus) {
            focus = newFocus;
            newFocus.OnFocused(transform);
            interactable = newFocus;
        }
    }

    private void RemoveFocus() {
        if (focus != null)
            focus.OnDeFocused();
        focus = null;

    }


    public void Interaction() {
        interactable.Interact(gameObject);
        //if (interactable.GetType()!=type.DoorInteractable)
        //interactable = null;
    }

    public void HoldDownInteraction() {
        interactable.HoldDownInteract();
    }
    public void HoldupInteraction() {
        interactable.HoldUpInteract();

    }
}
