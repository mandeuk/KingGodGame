﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithWorriorBase : EnemyBase {

    protected override void Init()
    {
        base.Init();
        base.maxHP = 100;
        base.curHP = 100;
        base.moveSpeed = 2;
        base.pushBack = 40;

        base.attackPower = 1f;
        base.attackDistance = 1.5f;
        base.stopDistance = 1f;
        base.findDistance = 7f;
    }
    
    // 코루틴이 포함되어 있어서 enable에서 이니셜라이즈를 해줘야함.
    // 방이 시작되기 전에는 방 전체를 Disable 하기 때문.
    private void OnEnable() 
    {
        Init();
    }
}