using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    [System.Serializable]
    public class Wave {

        public string waveName;
        public Transform enemy;
        public int amount;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountDown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    // Start is called before the first frame update
    void Start() {
        waveCountDown = timeBetweenWaves;
        if (spawnPoints.Length == 0) {
            Debug.LogError("No spawn points");
        }
    }

    // Update is called once per frame
    void Update() {
        if (state == SpawnState.WAITING) {
            if (!EnemyIsALive()) {
                WaveCompleted();
            }
            else {
                return;
            }

        }
        if (waveCountDown <= 0) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted() {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("All waves complete!! Looping");
        }
        else {
            nextWave++;
        }
    }

    bool EnemyIsALive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {

            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave wave) {
        Debug.Log("Spawning Wave" + wave.waveName);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < wave.amount; i++) {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy) {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        Instantiate(enemy, sp.position, sp.rotation);
        Debug.Log("Spawning Enemy : " + enemy.name);
    }

    public enum SpawnState { SPAWNING, WAITING, COUNTING }
}


