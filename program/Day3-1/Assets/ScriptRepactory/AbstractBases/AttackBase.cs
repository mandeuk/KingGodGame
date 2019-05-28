using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour{
    protected ObjectBase entity;
    protected DamageNode damageNode;
    protected Rigidbody rigid;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        entity.entityAttackCall += new AttackEventHandler(NormalAttack);
    }

    protected virtual void Init()
    {
        entity = GetComponent<ObjectBase>();
        rigid = GetComponent<Rigidbody>();
        damageNode = new DamageNode(entity.attackPower, entity.gameObject, 0.2f, entity.pushBack, (int)AttackType.Normal);
    }

    private void OnDisable()
    {
        entity.entityAttackCall -= new AttackEventHandler(NormalAttack);
    }

    public abstract void NormalAttack();
}