using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestBase : EnemyBase {
    public bool isDealTime, isWildAttack, isAttackReady, isLaserAttack;
    public Transform windEffectPos;
    GameObject wingEffect;

    protected override void Init()
    {
        base.Init();
        base.maxHP = 4000;
        base.curHP = 4000;
        base.moveSpeed = 1;
        base.pushBack = 50;

        base.attackPower = 1f;
        base.attackDistance = 9f;
        base.stopDistance = 8f;
        base.findDistance = 10f;

        isSuperArmor = true;
        isDealTime = false;
        isAttackReady = true;
        isLaserAttack = false;
        isWildAttack = false;

        wingEffect = Instantiate(Resources.Load("Prefabs/Enemy/HighPriestWing"), windEffectPos) as GameObject;
        //windEffectPos.GetComponent<HighPriestWing>().Pos = windEffectPos;
    }

    public override void Dead()
    {
        Destroy(wingEffect);
        base.Dead();
    }

    private void OnEnable()
    {
        Init();
    }
}
