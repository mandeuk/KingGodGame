using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTarBase : EnemyBase {
    bool isCorosusSpawn;

    protected override void Init()
    {
        base.Init();
        base.maxHP = 10;
        base.curHP = 10;
        base.moveSpeed = 1.2f;

        base.attackPower = 1;
        base.attackDistance = 2.4f;
        base.stopDistance = 2;
        base.findDistance = 10;
    }

    private void OnEnable()
    {
        Init();
    }

    public void AfterAttackDead()
    {
        GetComponent<IceTarHealth>().AfterAttackDeath();
    }
}
