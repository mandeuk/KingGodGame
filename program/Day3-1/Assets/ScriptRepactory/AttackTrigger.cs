using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNode
{
    public float damage;            // 데미지를 주는 주체의 공격력
    public GameObject attacker;     // 데미지를 주는 주체의 구별
    public float delay;             // 데미지의 간격
    public float pushBack;          // 데미지의 넉백 수치
    public int AttackType;          // 데미지의 구분.

    public DamageNode(float damage, GameObject attacker, float delay, float pushBack, int AttackType)
    {
        this.damage = damage;
        this.attacker = attacker;
        this.delay = delay;
        this.pushBack = pushBack;
        this.AttackType = AttackType;
    }

    public void SetNode(float damage, GameObject attacker, float delay, float pushBack, int attackType)
    {
        this.damage = damage;
        this.attacker = attacker;
        this.delay = delay;
        this.pushBack = pushBack;
        this.AttackType = attackType;
    }
}

public enum AttackType
{
    Normal = 1,
    SkillAttack  = 4,
    ChargeAttack = 3,
    SoulOfImpAttack = 5,
}

public enum Target        
{
    Player,
    Enemy,
    EnemyBullet,
    Obstacle,
    Other
}

public class AttackTrigger : MonoBehaviour {
    public DamageNode damageNode;
    public Target target;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(target.ToString()))
            return;

        ObjectBase objectBase = other.GetComponent<ObjectBase>();
        objectBase.Damaged(damageNode);
        // 이펙트 재생도..?
        // 이펙트를 타격, 피격, 허공 이펙트를 해야겠다.
        // 여기다가는 타격이펙트를 재생.
    }
}
