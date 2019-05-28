using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusBase : EnemyBase {
    public float dashSpeed;
    public int dashAttackCnt;
    public bool isBreak, isDealTime, isMovingJump;
    public Transform headObj;
    GameObject head;

    protected override void Init()
    {
        base.Init();
        base.maxHP = 3500;
        base.curHP = 3500;
        base.moveSpeed = 9;
        base.pushBack = 100;

        base.attackPower = 1f;
        base.attackDistance = 100f;
        base.stopDistance = 4f;
        base.findDistance = 12f;

        dashSpeed = 10;

        isSuperArmor = true;
        isBreak = false;
        isDealTime = false;
        isMovingJump = false;

        grade = Grade.Legendary;

        head = Instantiate(Resources.Load("Prefabs/Enemy/corosusFrostHead")) as GameObject;
        head.GetComponent<Corosushead>().headObj = headObj;
    }

    //public void DashAttack()
    //{
    //    GetComponent<CorosusAttack>().DashAttack();
    //}

    // 코루틴이 포함되어 있어서 enable에서 이니셜라이즈를 해줘야함.
    // 방이 시작되기 전에는 방 전체를 Disable 하기 때문.
    public override void Dead()
    {
        base.Dead();
        head.SetActive(false);
    }
    
    // Jump1을 위한 이벤트등록
    // 이게 불려지면 코로서스가 있는 맵에서 Tar가 떨어짐.

    private void OnEnable()
    {
        Init();
    }

    public void FootWalk()
    {
        SoundManager.playCorosusFootWalk();
    }

    public void HeadDestroy()
    {
        head.GetComponent<ParticleSystem>().Stop();
    }
}