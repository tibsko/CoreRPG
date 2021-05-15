using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour {
    public int attackDamages;
    public bool activateHitbox;
    public float attackRadius = 1f;
    public Transform target;

}
