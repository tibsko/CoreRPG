using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour {
    #region Singleton
    public static PlayerManager instance;

    public void Awake() {
        instance = this;
    }
    #endregion

    public GameObject player;

    private List<PlayerController> players;

    private void Start() {

        players = FindObjectsOfType<PlayerController>().ToList();

    }

    public PlayerController GetNearestPlayer(Vector3 pos) {
        return players.OrderBy(p => (p.transform.position - pos).magnitude).First();
    }

    public void AddPlayer() {

    }
}
