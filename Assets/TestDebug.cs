using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDebug : MonoBehaviour
{
    public GameObject zombieprefab;
    public Text zCountText;
    private int zCount = 0;

    public void SpawnZombie() {
        Instantiate(zombieprefab, transform.position, transform.rotation);
        zCount++;
        zCountText.text = zCount.ToString(); ;
    }
}
