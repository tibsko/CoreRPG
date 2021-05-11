﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    bool isFocus = false;
    Transform player;
   public bool hasInteracted = false;

    public string textButton;

    public virtual void Interact(GameObject player)
    {
        if (hasInteracted) {
        }
        //methode d'interaction
    }

    public virtual void HoldDownInteract() {

    }

    public virtual void HoldUpInteract() {

    }
    void Update()
    {
        
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        HUD.instance.ActivateButton(true);
        HUD.instance.NameButton(textButton);
    }


    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
        HUD.instance.ActivateButton(false);

    }
}
