using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosablePrevisualisation : MonoBehaviour {

    public bool CanPos { get; set; }

    [SerializeField] Material canPosMat;
    [SerializeField] Material notPosMat;
    [SerializeField] LayerMask layerMask;

    private Renderer renPosable;
    // Start is called before the first frame update
    void Start() {
        renPosable = GetComponent<Renderer>();
        renPosable.material = canPosMat;
        CanPos = true;

    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerStay(Collider other) {
        if (layerMask.ContainsLayer(other.gameObject.layer)&&other.gameObject.name!=gameObject.name) {
            renPosable.material = notPosMat;
            CanPos = false;
        }
    }
    void OnTriggerExit(Collider other) {
        if (layerMask.ContainsLayer(other.gameObject.layer)) {
            renPosable.material = canPosMat;
            CanPos = true;

        }
    }
}
