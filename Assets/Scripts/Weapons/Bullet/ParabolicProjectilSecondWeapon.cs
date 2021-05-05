using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectilSecondWeapon : MonoBehaviour
{
    private Vector3 endPosition;
    protected float animationProjectile;
    public Vector3 startPosition;

    [SerializeField] float speed;
    [SerializeField] float height;
    [SerializeField] GameObject smokeEffect;
    // Start is called before zthe first frame update
    void Start()
    {
        startPosition = PlayerManager.instance.GetNearestPlayer(transform.position).transform.position;
        endPosition = GetComponentInParent<PlayerSecondAttack>().targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        animationProjectile += Time.deltaTime;
        //animationProjectile = animationProjectile % speed;
        transform.position = ParabolaEquation.Parabole(startPosition, endPosition, height, animationProjectile/speed);
      
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
