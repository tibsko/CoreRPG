using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    void Awake() {
        if (HUD.instance==null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }


    [SerializeField] Button interactionButton;

    public void ActivateButton(bool state) {
        interactionButton.gameObject.SetActive(state);
    }

}
