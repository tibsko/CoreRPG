using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReferenceManager : MonoBehaviour {
    #region Singleton
    public static ReferenceManager instance;

    public void Awake() {
        instance = this;
    }
    #endregion

    public PlayerController Player { get; private set; }


    //public LayerMask groundLayer;
    //public LayerMask bulletLayer;
    public LayerMask playerLayer;
    //public LayerMask interactableLayer;
    public LayerMask enemyLayer;
    public LayerMask doorLayer;
    //public LayerMask collectableLayer;

    private List<PlayerController> players;

    private void Start() {
        Player = FindObjectOfType<PlayerController>();
        players = FindObjectsOfType<PlayerController>().ToList();
    }

    public PlayerController GetNearestPlayer(Vector3 pos, bool shouldBeAlive = true) {

        if (shouldBeAlive) {
            return Player;

            ////MULTIPLAYER
            //List<PlayerController> alivePlayers = new List<PlayerController>();
            //foreach (PlayerController player in players) {
            //    if (player.GetComponent<GenericHealth>().CurrentHealth > 0)
            //        alivePlayers.Add(player);
            //}
            //return alivePlayers.OrderBy(p => (p.transform.position - pos).magnitude).First();
        }
        else {
            return players.OrderBy(p => (p.transform.position - pos).magnitude).First();
        }
    }

    public void AddPlayer() {

    }
}
