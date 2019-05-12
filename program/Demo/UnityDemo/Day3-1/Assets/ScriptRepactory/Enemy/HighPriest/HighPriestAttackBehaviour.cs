using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestAttackBehaviour : StateMachineBehaviour {    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<HighPriestBase>().isAttack = true;
        animator.SetBool("Attack", false);
        animator.SetBool("LaserAttack", false);
        animator.SetBool("WildAttack", false);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<HighPriestBase>().isAttack = false;
    }
}
