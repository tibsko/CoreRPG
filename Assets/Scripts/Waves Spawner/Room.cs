using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField] bool firstRoom = false;

    private bool activated = false;
    private SpawnPoint[] spawnPoints;

    private void Start() {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        if (firstRoom) {
            ActivateRoom();
        }
    }

    public void ActivateRoom() {
        if (activated)
            return;

        activated = true;
        foreach (SpawnPoint spawn in spawnPoints) {
            WaveManager.instance.AddSpawner(spawn);
        }
    }
}
