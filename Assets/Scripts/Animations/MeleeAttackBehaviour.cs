using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour : StateMachineBehaviour {

    private PlayerAttack playerAttack;

    public bool lastCombo = false;

    public int damages;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!playerAttack)
            playerAttack = FindObjectOfType<PlayerAttack>();

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Weapon weapon = playerAttack.ActiveWeapon;
        if (weapon.GetType() == typeof(MeleeWeapon)) {
            MeleeWeapon melee = weapon as MeleeWeapon;
            melee.EndAttack(lastCombo);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
