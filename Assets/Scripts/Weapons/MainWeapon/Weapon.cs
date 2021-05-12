using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour {

    [Header("Weapon parameters")]
    public int damages;
    public float autoshootDistance;
    public AnimatorOverrideController overrideAnimator;

    public bool IsAttacking { get; set; }

    [HideInInspector] public UnityEvent onEndAttack;

    public abstract void Attack();
    public abstract bool CanAttack();


}



