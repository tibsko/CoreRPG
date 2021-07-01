using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoney : CollectableItem
{
    [SerializeField] int moneyAmount;
    [SerializeField] float checkRate;
    [SerializeField] float distanceMax;
    [SerializeField] float speedMove;

  
    private void Update() {
        MoveToPlayer();
    }

    public void Earn(GameObject player) {
        PlayerMoney playerMoney = player.GetComponent<PlayerMoney>();
        if (playerMoney) {
            playerMoney.AddMoney(moneyAmount);
        }
    }
    public override void Use(GameObject player) {
        Earn(player);
        Destroy(gameObject);
    }

    private void MoveToPlayer() {
        PlayerController player = ReferenceManager.instance.GetNearestPlayer(transform.position);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < distanceMax && this.enabled) {
            Vector3 direction = (player.transform.position-transform.position).normalized;
            transform.position += direction*speedMove*Time.deltaTime ;
        }

    }
}
