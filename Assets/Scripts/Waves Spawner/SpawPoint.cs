using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawPoint : MonoBehaviour
{
    private void OnTriggerExit(Collider enemy) {
        if (enemy.gameObject.layer == LayerManager.instance.enemyLayer) {
            enemy.GetComponent<EnemyController>().isInside = true;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
