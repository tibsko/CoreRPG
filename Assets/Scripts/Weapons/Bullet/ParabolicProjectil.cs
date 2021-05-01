using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectil : MonoBehaviour
{
    protected float animationProjectile;
    [SerializeField] float speed;
    [SerializeField] float height;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        animationProjectile += Time.deltaTime;
        animationProjectile = animationProjectile % speed;
        transform.position = ParabolaEquation.Parabole(startPosition.position, endPosition.position, height, animationProjectile/speed);
        
    }
}
