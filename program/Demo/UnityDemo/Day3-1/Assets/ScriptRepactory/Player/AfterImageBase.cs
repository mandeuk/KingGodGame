using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageBase : ObjectBase {

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Damaged(DamageNode damageNode)
    {
        throw new System.NotImplementedException();
    }

    public override void Dead()
    {
        throw new System.NotImplementedException();
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
