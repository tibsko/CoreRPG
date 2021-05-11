using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //[System.Serializable]
    //public class SpawnPoint {
    //    public Transform spawnerTransform;
    //    public BoxCollider boxCollider;
    //    public SpawnPoint spawnPoint;
    //}

    [SerializeField] List<GameObject> spawnPoints;

    public void ActiveRoomSpawners(bool state) {
        foreach(GameObject spawn in spawnPoints) {
            spawn.SetActive(state);
        }
    }
    //    // Start is called before the first frame update
    //    void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
