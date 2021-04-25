using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammeBullet : MonoBehaviour
{
    public FlammeThrowerData flammeThrowerData;

    private float delay=1f;
    private float countDown;
    private float stepDelay;

    public float startHeight=2.2f;
    public float endHeight = 8f;
    private float stepHeight;

    public float zPosCapsStart;
    public float zPosCapsEnd;
    public float zPosCapsStep;

    private CapsuleCollider capsule;
    // Start is called before the first frame update
    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
        stepDelay = delay/ (endHeight - startHeight);
        countDown = stepDelay;
        Debug.Log(stepDelay);

        stepHeight = (endHeight - startHeight)*(stepDelay);
        Debug.Log(stepHeight);
        capsule.height = startHeight;

        capsule.center = new Vector3(transform.position.x,transform.position.y,capsule.height*0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (capsule.height < endHeight) {
            countDown -= Time.deltaTime;
            if (countDown <= 0) {
                capsule.height += stepHeight;
                countDown = stepDelay;
            }
        }
        else {
            capsule.height = endHeight; 
        }
        capsule.center = new Vector3(0,0,capsule.height*0.5f);


    }
    void OnTriggerStay(Collider collider) {
        EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth) {
            StartCoroutine(DamageTime(enemyHealth));
        }


    }
    
    IEnumerator DamageTime(EnemyHealth enemy) {
        yield return new WaitForSeconds(flammeThrowerData.damageSpeed);
        enemy.TakeDamage(flammeThrowerData.damages, gameObject);
    }
}
