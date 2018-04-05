using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAttackBehaviour : StateMachineBehaviour {
    
    private float attackTime = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        attackTime = Time.time;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        if (attackTime + 0.75f > Time.time)  
            // 이번 프레임이 시작한 시간 + 0.75초 동안 어택을 스탑하고 그다음부터 어택가능한상태로 만듬
        {
            animator.SetBool("Combo", false);
            //playerAttack.StopAttacking();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
