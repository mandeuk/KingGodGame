using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusMovement : EnemyMovement{
    CorosusBase corosusEntity;

    // Use this for initialization
    void Awake()
    {
        Init();
        corosusEntity = entity as CorosusBase;
    }

    protected override void Init()
    {
        base.Init();
    }
    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
    }

    // 점프끝나고 멍때리는시간 있어야해서 여기다가 오버라이드함.
    public override void MoveUpdate()
    {
        if (enemyEntity.isAgro)
        {
            if (nav.enabled && !enemyEntity.isAttack && !enemyEntity.isDead && !corosusEntity.isDealTime)
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
}
