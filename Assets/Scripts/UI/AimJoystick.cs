using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimJoystick : MonoBehaviour {

    [SerializeField] RawImage baseImg;
    [SerializeField] RawImage joystickBtn;
    [SerializeField] float radius;

    private PlayerAttack player;

    // Start is called before the first frame update
    void Start() {
        Toggle(false);
        player = FindObjectOfType<PlayerAttack>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePos = Input.mousePosition;

        //Base Show & Postion
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            baseImg.rectTransform.position = mousePos;
            Toggle(true);
        }

        //float angle=0;

        //Position Joystick
        if (Input.GetKey(KeyCode.Mouse0)) {
            Vector3 baseToMouse = mousePos - baseImg.rectTransform.position;
            float dist = baseToMouse.magnitude;


            if (dist > radius) {
                Vector3 clampPos = baseImg.rectTransform.position + baseToMouse.normalized * radius;
                joystickBtn.rectTransform.position = clampPos;
            }
            else
                joystickBtn.rectTransform.position = mousePos;
        }
        else {
            joystickBtn.rectTransform.position = baseImg.rectTransform.position;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            //player.AimAndShoot();
            Toggle(false);
        }

    }

    private void Toggle(bool toggle) {
        joystickBtn.enabled = toggle;
        baseImg.enabled = toggle;
    }
}
