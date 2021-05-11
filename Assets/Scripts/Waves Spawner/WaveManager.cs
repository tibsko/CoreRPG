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
    private int enemyCount = 0;
    private float totalProbability = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        spawnPoints = FindObjectsOfType<SpawnPoint>().ToList();
        SpawnData = Instantiate(baseSpawnData);

        if (spawnPoints.Count == 0) {
            Debug.LogError("No SpawnPoint has been found in WaveManager.");
        }

        InvokeRepeating(nameof(SearchForEnemy), 0, searchRate);
    }

    public void AddSpawner(SpawnPoint spawnPoint) {
        if (!spawnPoints.Contains(spawnPoint)) {
            spawnPoints.Add(spawnPoint);
        }
    }

    private void SearchForEnemy() {
        if (GameObject.FindGameObjectWithTag("Enemy") == null) {
            waveEndedSignal++;
        }

        if (waveEndedSignal > 1) {
            waveEndedSignal = 0;
            Invoke(nameof(StartNewWave), timeBetweenWaves);
        }
    }

    private void StartNewWave() {
        WaveNumber++;
        totalProbability = 0;
        foreach (EnemyProbability prob in SpawnData.enemyRates) {
            if (prob.startWave >= WaveNumber) {
                prob.Increment();
                prob.CurrentProbability = Random.Range(prob.minProbability, prob.maxProbability);
                totalProbability += prob.CurrentProbability;
            }
        }

        InvokeRepeating(nameof(SpawnEnemy), 0, SpawnData.spawnRate);
    }

    private void SpawnEnemy() {

        if (enemyCount >= SpawnData.maxSimultaneaousEnemies) {
            Debug.Log($"Max simultaneous enemy count reached ({enemyCount})");
            return;
        }

        if (enemyCount >= SpawnData.enemyAmount) {
            Debug.Log($"All enemies have been spawned");
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        GameObject enemy = null;
        float rand = Random.Range(0, totalProbability);
        float cursor = 0;
        foreach (EnemyProbability prob in SpawnData.enemyRates) {
            if (prob.startWave >= WaveNumber) {
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
            Debug.Log($"Spawning Enemy {enemy.name} on {sp.name} ");
            enemyCount++;
        }
    }
}
