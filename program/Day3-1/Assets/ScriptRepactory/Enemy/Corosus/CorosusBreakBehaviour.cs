using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusBreakBehaviour : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.transform.GetComponent<CorosusAttack>().Break();
    }
}