using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoystickInput : MonoBehaviour {

    private Joystick joystick;

    [System.Serializable]
    public class DragJoystickEvent : UnityEvent<Vector2> { }

    public DragJoystickEvent onDragJoystick;

    // Start is called before the first frame update
    void Start() {
        joystick = GetComponent<Joystick>();

        if (joystick == null) {
            Debug.LogError("Can't find joystick");
        }
    }

    // Update is called once per frame
    void Update() {
        onDragJoystick.Invoke(joystick.Direction);
    }

    private void OnGUI() {
        GUI.Label(new Rect(20, 50, 500, 30), $"{gameObject.name} : Direction=[{joystick.Direction}]");
    }
}
