using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardOverrideInput : MonoBehaviour {

    [SerializeField] bool overrideInput = false;

    [System.Serializable]
    public class DragJoystickEvent : UnityEvent<Vector2> { }
    public DragJoystickEvent onDragJoystick;

    private Joystick joystickToReplace;
    private Vector2 inputs;


    // Start is called before the first frame update
    void Start() {
        joystickToReplace = GetComponent<Joystick>();

        if (joystickToReplace == null) {
            overrideInput = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (overrideInput) {
            inputs.x = Input.GetAxis("Horizontal");
            inputs.y = Input.GetAxis("Vertical");
            onDragJoystick.Invoke(new Vector2(inputs.x, inputs.y));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            OverrideInput(true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            OverrideInput(false);
        }
    }

    private void OverrideInput(bool overrideStatus) {

        if (joystickToReplace != null) {
            overrideInput = overrideStatus;
            joystickToReplace.enabled = !overrideStatus;
        }
        else
            overrideInput = true;

    }

    private void OnGUI() {
        if (overrideInput) {
            GUI.Label(new Rect(20, 50, 500, 30), $"{gameObject.name} : Direction=[{inputs}]");

        }
    }
}
