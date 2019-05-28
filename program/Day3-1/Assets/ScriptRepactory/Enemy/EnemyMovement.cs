using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MoveBase {
    protected EnemyBase enemyEntity;
    protected GameObject player;
    protected NavMeshAgent nav;
    protected Animator anim;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        enemyEntity = entity as EnemyBase;
        player = enemyEntity.player;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public virtual void MoveUpdate()
    {
        if (enemyEntity.isAgro)
        {   // 턴하는 도중에 움직이는 이유는 도는 애니메이션이 따로 없어서 걷는애니메이션으로 도는것처럼 하기위함.
            if (nav.enabled && !enemyEntity.isAttack && !enemyEntity.isDead/* && !enemyEntity.isTurn*/)
            {
                nav.destination = player.transform.position;

                if (Vector3.Distance(player.transform.position, transform.position) > enemyEntity.stopDistance)
                {
                    StartMove();
                }
                else
                {
                    StopMove();
                }
            }
            else
            {
                StopMove();
            }
        }
    }

    public void StopMove()
    {
        nav.speed = 0;
        enemyEntity.isMove = false;
        anim.SetBool("Walk", false);
    }

    public void StartMove()
    {
        nav.speed = enemyEntity.moveSpeed;
        enemyEntity.isMove = true;
        anim.SetBool("Walk", true);
    }

    public void StopWalk()
    {
        nav.speed = 0;
    }
}
