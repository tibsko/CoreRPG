using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : MonoBehaviour
{
    private Vector3 endPosition;
    protected float animationProjectile;
    public Transform startPosition;

    [SerializeField] float speed;
    [SerializeField] float height;
    [SerializeField] GameObject smokeEffect;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = GetComponentInParent<SpitterAttack>().target.position;
    }

    // Update is called once per frame
    void Update()
    {
        animationProjectile += Time.deltaTime;
        //animationProjectile = animationProjectile % speed;
        transform.position = ParabolaEquation.Parabole(startPosition.position, endPosition, height, animationProjectile/speed);
      
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.name);
        if (collider.gameObject.layer == 9 || collider.gameObject.layer == 8) {
            GameObject poisonEffect = Instantiate(smokeEffect, new Vector3(transform.position.x,collider.transform.position.y,transform.position.z), Quaternion.identity);
            Destroy(poisonEffect, 5f);
            Destroy(gameObject);
        }
        PlayerHealth player = collider.GetComponent<PlayerHealth>();
        if (player) {
            //take damages
        }
        
    }
}
