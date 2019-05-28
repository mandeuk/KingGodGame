using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestMovement : EnemyMovement {
    HighPriestBase priestEntity;
    
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        priestEntity = entity as HighPriestBase;
    }

    private void FixedUpdate()
    {
        base.MoveUpdate();
    }

    public override void MoveUpdate()
    {
        if (enemyEntity.isAgro)
        {
            if (nav.enabled && !enemyEntity.isAttack && !enemyEntity.isDead && !priestEntity.isDealTime)
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
