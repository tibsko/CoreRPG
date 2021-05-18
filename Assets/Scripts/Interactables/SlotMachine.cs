using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] List<GameObject> rolls = new List<GameObject>();
    [SerializeField] float speedRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RotateRolls() {
        foreach (GameObject roll in rolls) {
            roll.transform.Rotate(Vector3.up, speedRotation*Time.deltaTime);
        }
    }
}
