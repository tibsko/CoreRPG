using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour {
    public Text text;
    // Update is called once per frame
    void Update() {
        text.text = $"{(int)(1.0f / Time.deltaTime)}";
    }
}
