﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    [SerializeField] LootRate[] Loots;
    [SerializeField] GameObject coin;

    public void InctantiateLoot() {

        int rand = Random.Range(0, 100);
        int total = 0;
        Instantiate(coin,
                    transform.position + new Vector3(Random.Range(0f, 1f),
                    0,
                    Random.Range(0f, 1f)),
                    Quaternion.identity);
        for (int i = 0; i < Loots.Length; i++) {
            total += Loots[i].rate;
            if (rand < total) {
                Instantiate(Loots[i].collectable,
                    transform.position + new Vector3(Random.Range(0f, 1f),
                    0,
                    Random.Range(0f, 1f)),
                    Quaternion.identity);
                break;
            }
        }

    }
}

[System.Serializable]
public struct LootRate {
    public GameObject collectable;
    public int rate;
}