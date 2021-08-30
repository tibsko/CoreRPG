using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //[SerializeField] Transform target;
    //Vector3 offsetCamera;

    //[Range(0.01f, 1.0f)]
    //[SerializeField] float smooth;

    //void Start() {
    //    offsetCamera = transform.position - target.position;
    //}

    //void Update() {
    //    Vector3 cameraPosition = target.position + offsetCamera;
    //    Vector3 smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smooth);
    //    transform.position = smoothPosition;
    //    transform.LookAt(target);
    //}
    [SerializeField] Transform target;
    [SerializeField] Vector3 offSetCam;

    [SerializeField] float rotationCamX;
    [SerializeField] float followingStep = 0.1f;
    [SerializeField] float rotationSpeed = .1f;

    public Transform obstruction;
    private Vector3 velocity;

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
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offSetCam, ref velocity, followingStep);
      
        transform.rotation = Quaternion.AngleAxis(rotationCamX,Vector3.right);
        //this.transform.LookAt(target);
        //this.transform.forward = target.forward;
        //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(rotationCamX, transform.rotation.y, 0, 0), rotationSpeed);
    }

    ////void ViewObstructed() {
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
