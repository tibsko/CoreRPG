using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSpawnerData", menuName = "ScriptableObjects/WaveSpawnerData")]
public class WaveSpawnerData : ScriptableObject
{
    [Header("Standard Rates")]
    public EnemyProbability[] enemyRates;
    public float spawnRate = 1f;
    public int maxSimultaneaousEnemies = 100;
    public int enemyAmount = 10;

    [Space]
    [Header("Special waves")]
    public List<OverrideWave> specialWaves;
}

[System.Serializable]
public class EnemyProbability {

    public GameObject enemyPrefab;
    public int startWave;
    public float minProbability;
    public float maxProbability;
    [SerializeField] float incrementMaxProbability;
    [SerializeField] float incrementMinProbability;

    public float CurrentProbability { get; set; }

    public void Increment() {
        minProbability += incrementMinProbability;
        maxProbability += incrementMaxProbability;
    }
}

[System.Serializable]
public class OverrideWave {

    public string waveName;
    public GameObject enemyPrefab;
    public int startWave;
    public int waveMultiple;
    public int amount;
    public float minProbability;
    public float maxProbability;
    public float rate;
}

[System.Serializable]
public struct EnemyR {
    public Transform enemyTransform;
    public int enemyRate;
}