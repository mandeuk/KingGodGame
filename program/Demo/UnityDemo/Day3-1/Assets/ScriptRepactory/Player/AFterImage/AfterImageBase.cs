using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageBase : ObjectBase {

    public override void Attack()
    {

    }

    public override void Damaged(DamageNode damageNode)
    {

    }

    public override void Dead()
    {

    }

    protected override void Init()
    {
        base.Init();
        maxHP = 0;
        curHP = 0;
        attackPower = 0;
        moveSpeed = 0;
        pushBack = 0;
    }
}
