using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusJumpAttack2_2Behaviour : StateMachineBehaviour {
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ObjectBase>().isAttack = false;
    }
}