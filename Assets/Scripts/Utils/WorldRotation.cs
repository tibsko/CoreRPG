using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotation : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(new Vector3(0, 45,0));
    }
}
