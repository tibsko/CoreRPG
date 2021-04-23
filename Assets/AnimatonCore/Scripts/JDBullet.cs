using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JDBullet : MonoBehaviour
{
    public float velocity;
    private Rigidbody rigidbody;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }
}
