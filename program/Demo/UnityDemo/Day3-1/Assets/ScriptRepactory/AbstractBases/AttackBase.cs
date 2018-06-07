using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour{
    protected ObjectBase entity;
    protected DamageNode damageNode;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        while (entity)
        {
            entity = GetComponent<ObjectBase>();
            damageNode = new DamageNode(entity.attackPower, entity.gameObject, 0.2f, entity.pushBack, 1);
        }
    }

    public abstract void NormalAttack();

}