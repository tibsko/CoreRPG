using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieAttack : MonoBehaviour {

    public int attackDamages;
    [SerializeField] protected float attackRadius = 1f;

    public GenericHealth Target { get => zombieController.Target; }


    protected Animator animator;
    protected ZombieController zombieController;

    protected void Start() {
        zombieController = GetComponent<ZombieController>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    //void OnTriggerEnter(Collider collision) {
    //    PlayerAttack player = collision.GetComponent<PlayerAttack>();
    //    if (player) {
    //        transform.LookAt(player.transform.position);
    //    }
    //}

    protected abstract void Update();
    protected abstract void DieBehaviour();
}
