using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardOverrideInput : MonoBehaviour {

    private JoystickInput joystickToReplace;

    private Vector2 inputs;

    // Start is called before the first frame update
    void Start() {
        joystickToReplace = GetComponent<JoystickInput>();

        if (joystickToReplace != null) {
            joystickToReplace.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
        inputs.x = Input.GetAxis("Horizontal"); 
        inputs.y = Input.GetAxis("Vertical"); 
        joystickToReplace.onDragJoystick.Invoke(inputs);
    }

    private void OnGUI() {
        GUI.Label(new Rect(20, 50, 500, 30), $"{gameObject.name} : Direction=[{inputs}]");
    }
}
