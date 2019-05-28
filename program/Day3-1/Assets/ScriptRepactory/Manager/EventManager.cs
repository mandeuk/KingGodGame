using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Delegate 모양을 잡아줌.
// 이벤트 매니저에 등록될 Delegate들
public delegate void PlayerAttackEventHandler(int stateNum);
public delegate void EnemyHitEventHandler(int stateNum, GameObject enemy);
public delegate void CameraMoveEventHandler(int stateNum);

// 이벤트 매니저 밖에 등록될, ObjectBase에 등록될 Delegate들
public delegate void DeadEventHandler();
public delegate void DamagedEventHandler(DamageNode damageNode);
public delegate void AttackEventHandler();

public delegate void CorosusJumpAttackEventHandler();

public delegate void SkillAttackEventHandler(DamageNode damageNode);


// 여기랑 코드마다 따로 실행하는 이유는 헷갈릴까봐임
// EventManger.AttackEvent 처럼 쓰는것과 그냥 바로 AttackEvent로 쓰는건 차이가 있기때문.
// 명확히 하기위함... 일까요 ㅠ

public static class EventManager  {
    public static event PlayerAttackEventHandler AttackEventCall;   // 로투스땜에 여기뒀었음.
    public static event EnemyHitEventHandler EnemyHitEventCall;
    public static event CameraMoveEventHandler CameraMoveEventCall;
    public static event CorosusJumpAttackEventHandler jumpAttackEventCall;

    public static void AttackEvent(int stateNum)
    {
        if (AttackEventCall != null)
        {
            AttackEventCall(stateNum);
        }
    }

    public static void EnemyHitEvent(int stateNum, GameObject enemy)
    {
        if (EnemyHitEventCall != null)
        {
            EnemyHitEventCall(stateNum, enemy);
        }
    }

    public static void CameraMoveEvent(int stateNum)
    {
        if (CameraMoveEventCall != null)
        {
            CameraMoveEventCall(stateNum);
        }
    }

    public static void CorosusJump1Attack()
    {
        if (jumpAttackEventCall != null)
            jumpAttackEventCall();
    }

}
