using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {
    private float attackTime = 0;
    private PlayerAttack playerattack;
    private Rigidbody rigidb;

    private void Awake()
    {
        playerattack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        rigidb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        playerattack.b_attacking = true;
        playerattack.StopAttacking();
        attackTime = Time.time;
        //animator.speed = animator.transform.GetComponent<PlayerStatus>().attackSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //playerattack.b_attacking = true;
        if (attackTime + 0.3f > Time.time)
        {
            animator.SetBool("Combo", false);
        }

        //if (animator.GetBool("Dash") && animator.GetFloat("DashTiming") > 0.1)
        //{
        //    animator.transform.GetComponent<PlayerEffect>().playBlinkEffect();
        //}
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //playerattack.b_attacking = false;
    }
}
