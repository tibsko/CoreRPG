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
    [SerializeField] Button interactionButton2;
    [SerializeField] Text textMoney;

    public void ActivateButton(bool state) {
        interactionButton.gameObject.SetActive(state);

    }

    public void ActivateButton2(bool state) {
        interactionButton2.gameObject.SetActive(state);

    }
    public void NameButton(string name) {
        interactionButton.gameObject.GetComponentInChildren<Text>().text = name;

    }

    public void UpdateMoney(int money) {
        textMoney.text = ""+ money;
    }

}
