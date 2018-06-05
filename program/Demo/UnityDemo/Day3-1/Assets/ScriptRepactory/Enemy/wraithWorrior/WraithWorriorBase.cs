using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithWorriorBase : EnemyBase {

    protected override void Init()
    {
        base.Init();
        base.maxHP = 100;
        base.curHP = 100;
        base.moveSpeed = 2;

        base.attackPower = 1;
        base.attackDistance = 2;
        base.stopDistance = 2;
        base.findDistance = 4;
    }
    
    // 코루틴이 포함되어 있어서 enable에서 이니셜라이즈를 해줘야함.
    // 방이 시작되기 전에는 방 전체를 Disable 하기 때문.
    private void OnEnable() 
    {
        Init();
    }
}
