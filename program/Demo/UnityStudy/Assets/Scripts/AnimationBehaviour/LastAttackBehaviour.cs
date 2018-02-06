using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAttackBehaviour : StateMachineBehaviour {

    private PlayerAttack playerAttack;
    private float attackTime = 0;
    private Animator anim;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
        attackTime = Time.time;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        if (attackTime + 0.5f > Time.time)  
            // 이번 프레임이 시작한 시간 + 0.5초 동안 어택을 스탑하고 그다음부터 어택가능한상태로 만듬
        {
            playerAttack.StopAttacking();
        }

        if(Mathf.Approximately(attackTime + 1.0f,  Time.deltaTime))
        {

            Debug.Log("123");
            playerAttack.NormalAttack();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
