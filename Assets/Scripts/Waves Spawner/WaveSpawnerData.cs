using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSpawnerData", menuName = "ScriptableObjects/WaveSpawnerData")]
public class WaveSpawnerData : ScriptableObject {

    [Header("Standard Rates")]
    public float spawnRate = 1f;
    public int maxSimultaneaousEnemies = 100;
    public EnemyProbability[] enemyRates;

    [Header("Enemy amount increase")]
    [SerializeField] EFunctionType increaseFunction;
    [SerializeField] float amountModifier = 1;
    public int EnemyAmount { get; private set; }

    [Space]
    [Header("Special waves")]
    public List<OverrideWave> specialWaves;


    public void ComputeAmount(int waveNumber) {
        float result = 0f;
        switch (increaseFunction) {
            case EFunctionType.Linear:
                result = waveNumber * amountModifier;
                break;
            case EFunctionType.Log:
                result = Mathf.Log(waveNumber * amountModifier);
                break;
            case EFunctionType.Pow:
                result = Mathf.Pow(waveNumber, amountModifier);
                break;
            case EFunctionType.Exp:
                result = Mathf.Exp(waveNumber * amountModifier);
                break;
        }
        EnemyAmount = Mathf.CeilToInt(result);
    }

    public enum EFunctionType {
        Linear,
        Log,
        Pow,
        Exp
    }
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