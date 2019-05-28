﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBossBase : EnemyBase {

    public bool isAttackTiming, isAttackReady;
    public float moveAttackDistance;


    protected override void Init()
    {
        base.Init();
        base.maxHP = 600;
        base.curHP = 600;
        base.moveSpeed = 2;
        base.pushBack = 40;

        base.attackPower = 1;
        base.attackDistance = 9;
        base.stopDistance = 7;
        base.findDistance = 13;

        isAttackReady = true;
    }

    private void OnEnable()
    {
        Init();
    }

    IEnumerator AttackReadyPattern()
    {
        while (!isAttackReady)
        {

            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
