using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour {
    public float maxHP, curHP, attackPower, moveSpeed, pushBack;
    public bool isDead, isAttack, isMove, isDamaged, isTurn, isSuperArmor, isInvincibility;
    
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

    public abstract void Dead();
    public abstract void Damaged(DamageNode damageNode);
    public abstract void Attack();    
}
