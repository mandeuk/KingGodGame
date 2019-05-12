using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 이벤트매니저에 등록해서 사용할 수 없음. call은 객체마다 해야하는데 eventManager에서 콜하는건
// 전체 다 불리기때문.. 본인의 이펙트나 본인이 챙길건 본인이 신호를 줘야하는데
// 공용적인 eventManager에서 부르게되면 같은걸 모두가 쓰기때문에 안됨..

// 심지어 얘는 아이템 소울오브임프때문에 만든건데 이 이벤트 콜을 아이템이 구독하게 할 방법이 없음
// 그냥 OOP를 위한 용도로만 사용할 예정.

// 추가할 것 : 엔티티와 묶여있는 어택,무브,헬스 를 각각 할당해주고 붙었는지 안붙었는지 판단까지 하게끔

public class ObjectBase : MonoBehaviour {
    public float maxHP, curHP, attackPower, moveSpeed, pushBack;
    public bool isDead, isAttack, isMove, isDamaged, isTurn, isSuperArmor, isInvincibility;

    public event DeadEventHandler entityDeadCall;

    public event DamagedEventHandler entityDamagedCall;

    public event AttackEventHandler entityAttackCall;

    protected virtual void Init()
    {
        isDead = false;
        isAttack = false;
        isMove = false;
        isDamaged = false;
        isTurn = false;
        isSuperArmor = false;
        isInvincibility = false;
    }

    public virtual void Dead()
    {
        if (entityDeadCall != null)
        {
            entityDeadCall();
        }
    }

    public virtual void Damaged(DamageNode damageNode)
    {
        if (entityDamagedCall != null)
        {
            entityDamagedCall(damageNode);
        }
    }
    
    public virtual void Attack()
    {
        if (entityAttackCall != null)
        {
            entityAttackCall();
        }
    }

    public virtual void InvincibilityOn()
    {
        isInvincibility = true;
    }

    public virtual void InvincibilityOff()
    {
        isInvincibility = false;
    }
}
