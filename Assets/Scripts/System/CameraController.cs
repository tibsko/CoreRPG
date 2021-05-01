using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSetCam;

    [SerializeField] float rotationCamX;
    [SerializeField] float followingStep = 0.1f;


    public Transform obstruction;

    // Start is called before the first frame update
    void Start() {
        obstruction = target;
        if (target == null) {
            target = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Update is called once per frame
    void Update() {
        CameraMove();
        //ViewObstructed();
    }

    void CameraMove() {
        transform.position = Vector3.Lerp(transform.position, target.position + offSetCam, followingStep);
        transform.rotation = Quaternion.Euler(rotationCamX, 0, 0);
    }

    //void ViewObstructed() {
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, target.position - transform.position, out hit)) {
    //        if (hit.collider.gameObject.layer != target.gameObject.layer) {
    //            obstruction = hit.transform;
    //            if (obstruction.gameObject.GetComponent<MeshRenderer>()) {
    //                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

    //            }
    //        }
    //        else {
    //            if (obstruction.gameObject.GetComponent<MeshRenderer>()) {
    //                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

    //            }
    //        }
    //    }
    //}
}
