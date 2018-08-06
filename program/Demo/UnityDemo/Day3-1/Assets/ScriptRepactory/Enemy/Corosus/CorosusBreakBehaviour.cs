using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusBreakBehaviour : StateMachineBehaviour {
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.GetComponent<ObjectBase>().isAttack = false;
        //animator.SetBool("Attack", false);
    }
}
