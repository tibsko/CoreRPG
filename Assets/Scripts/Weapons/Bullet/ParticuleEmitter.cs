using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleEmitter : MonoBehaviour
{
    public GameObject particule;
    
    public void InstantiateParticule(Vector3 source) {
        
        GameObject particuleClone = Instantiate(particule,source,Quaternion.identity) ;

        Destroy(particuleClone, 3f);
    }
}
