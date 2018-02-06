using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour {
    protected Animator avatar;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        avatar = GameObject.Find("Player").GetComponent<Animator>();
        avatar.SetBool("StartAttack", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
