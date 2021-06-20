using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoney : CollectableItem
{
    [SerializeField] int moneyAmount;
    [SerializeField] float checkRate;
    [SerializeField] float distanceMax;
    [SerializeField] float speedMove;

    void Start() {
        InvokeRepeating(nameof(MoveToPlayer), 0, checkRate);
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
            transform.position = Vector3.Lerp(transform.position, player.transform.position,speedMove);
        }

    }
}
