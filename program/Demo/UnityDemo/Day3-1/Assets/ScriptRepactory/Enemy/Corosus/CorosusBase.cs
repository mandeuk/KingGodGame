using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusBase : EnemyBase {
    public float dashSpeed;
    public bool isBreak, isDealTime, isJumpAttack, isAttackReady;
    public Transform headObj;

    protected override void Init()
    {
        base.Init();
        base.maxHP = 100;
        base.curHP = 100;
        base.moveSpeed = 3;
        base.pushBack = 40;

        base.attackPower = 1f;
        base.attackDistance = 6f;
        base.stopDistance = 4f;
        base.findDistance = 8f;

        dashSpeed = 10;

        isSuperArmor = true;
        isBreak = false;
        isDealTime = false;
        isJumpAttack = false;
        isAttackReady = true;

        GameObject head = Instantiate(Resources.Load("Prefabs/Enemy/corosusHead"), transform) as GameObject;
        head.GetComponent<Corosushead>().headObj = headObj;
    }

    public void DashAttack()
    {
        GetComponent<CorosusAttack>().DashAttack();
    }

    // 코루틴이 포함되어 있어서 enable에서 이니셜라이즈를 해줘야함.
    // 방이 시작되기 전에는 방 전체를 Disable 하기 때문.
    private void OnEnable()
    {
        Init();
    }
}
