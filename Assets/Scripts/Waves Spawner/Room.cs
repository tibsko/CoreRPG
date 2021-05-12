using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField] bool firstRoom = false;

    private SpawnPoint[] spawnPoints;

    private void Start() {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        if (firstRoom) {
            ActivateRoom();
        }
    }

    public void ActivateRoom() {
        foreach (SpawnPoint spawn in spawnPoints) {
            WaveManager.instance.AddSpawner(spawn);
        }
    }
}
