using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDownCamera : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSetCam;

    [SerializeField] float rotationCamX;
    // Start is called before the first frame update
    void Start() {
        if (target == null) {
            target = FindObjectOfType<JDPlayerController>().transform;
        }
    }

    // Update is called once per frame
    void Update() {
        CameraMove();
    }

    void CameraMove() {
        transform.position = target.position + offSetCam;
        transform.rotation = Quaternion.Euler(rotationCamX, 0, 0);
    }
}
