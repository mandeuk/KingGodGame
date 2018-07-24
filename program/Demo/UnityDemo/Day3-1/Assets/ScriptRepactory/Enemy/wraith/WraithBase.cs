using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBase : EnemyBase {
    public float bulletSpeed;

    protected override void Init()
    {
        base.Init();
        base.maxHP = 100;
        base.curHP = 100;
        base.moveSpeed = 1;

        base.attackPower = 1;
        base.attackDistance = 6;
        base.stopDistance = 5;
        base.findDistance = 10;
    }

    private void OnEnable()
    {
        Init();
    }
}
