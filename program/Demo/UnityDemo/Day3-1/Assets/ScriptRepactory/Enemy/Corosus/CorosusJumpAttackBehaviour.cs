﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusJumpAttackBehaviour : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        animator.transform.GetComponent<ObjectBase>().isAttack = true;
    }
}