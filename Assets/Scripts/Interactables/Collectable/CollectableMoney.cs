using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoney : CollectableItem
{
    [SerializeField] int moneyAmount;
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
}
