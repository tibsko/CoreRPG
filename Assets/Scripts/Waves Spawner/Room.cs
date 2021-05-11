using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField] List<SpawnPoint> spawnPoints;

    public void ActivateRoom() {
        foreach (SpawnPoint spawn in spawnPoints) {
            WaveManager.instance.AddSpawner(spawn);
        }
    }
}
