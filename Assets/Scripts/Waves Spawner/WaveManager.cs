using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public static WaveManager instance;

    [SerializeField] WaveSpawnerData baseSpawnData;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float searchRate = 1f;

    public WaveSpawnerData SpawnData { get; set; }
    public int WaveNumber { get; set; }

    private List<SpawnPoint> spawnPoints;
    private int waveEndedSignal = 0;
    private int enemySpawned = 0;
    private float totalProbability = 0;
    private bool spawning = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

        spawnPoints = new List<SpawnPoint>();
        SpawnData = Instantiate(baseSpawnData);
    }

    void Start() {
        InvokeRepeating(nameof(SearchForEnemy), 0, searchRate);
    }

    public void AddSpawner(SpawnPoint spawnPoint) {
        if (!spawnPoints.Contains(spawnPoint)) {
            spawnPoints.Add(spawnPoint);
            Debug2.Log($"Adding spawner : {spawnPoint.name}");

        }
    }

    private void SearchForEnemy() {
        if (spawning)
            return;

        if (GameObject.FindGameObjectWithTag("Enemy") == null) {
            waveEndedSignal++;
            Debug2.Log("No enemy found");
        }

        if (waveEndedSignal > 1 && spawnPoints.Count > 0) {
            waveEndedSignal = 0;
            spawning = true;
            Invoke(nameof(StartNewWave), timeBetweenWaves);
        }
    }

    private void StartNewWave() {
        WaveNumber++;
        totalProbability = 0;
        enemySpawned = 0;
        spawning = true;
        Debug2.Log($"Starting new wave n° {WaveNumber}");

        //Update spawn rate for each enemy type
        foreach (EnemyProbability prob in SpawnData.enemyRates) {
            if (prob.startWave >= WaveNumber) {
                if (prob.startWave != WaveNumber)
                    prob.Increment();
                prob.CurrentProbability = Random.Range(prob.minProbability, prob.maxProbability);
                totalProbability += prob.CurrentProbability;
            }
        }

        //Update spawn amount
        SpawnData.ComputeAmount(WaveNumber);
        Debug2.Log($"Preparing to spawn {SpawnData.EnemyAmount} enemies");
        //Spawn enemies
        InvokeRepeating(nameof(SpawnEnemy), 0, SpawnData.spawnRate);
    }

    private void SpawnEnemy() {

        //if (enemyCount >= SpawnData.maxSimultaneaousEnemies) {
        //    Debug2.Log($"Max simultaneous enemy count reached ({enemyCount})");
        //    return;
        //}

        if (enemySpawned >= SpawnData.EnemyAmount) {
            Debug2.Log($"All enemies have been spawned. Stopping spawn.");
            spawning = false;
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        GameObject enemy = null;
        float rand = Random.Range(0, totalProbability);
        float cursor = 0;
        foreach (EnemyProbability prob in SpawnData.enemyRates) {
            if (WaveNumber >= prob.startWave) {
                cursor += prob.CurrentProbability;
                if (rand <= cursor) {
                    enemy = prob.enemyPrefab;
                    break;
                }
            }
        }

        if (enemy != null) {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
            Instantiate(enemy, sp.position, sp.rotation);
            Debug2.Log($"Spawning enemy ({enemy.name}) on {sp.name} ");
            enemySpawned++;
        }
    }
}
