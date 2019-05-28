using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusAttackBehaviour : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetComponent<CorosusAttack>().Dash();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.transform.GetComponent<ObjectBase>().isAttack = false;
        animator.SetBool("Attack", false);
    }
}
