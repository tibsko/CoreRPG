using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{

    public Animator animator;
    private bool isShooting = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("IsShooting="+ isShooting);
            isShooting = !isShooting;
            animator.SetTrigger("Shoot");
        }
    }
}
