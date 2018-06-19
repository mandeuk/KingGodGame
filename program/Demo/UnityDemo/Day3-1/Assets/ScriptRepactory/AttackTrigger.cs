using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNode
{
    public float damage;
    public GameObject attacker;
    public float delay;
    public float pushBack;
    public int AttackType;

    public DamageNode(float damage, GameObject attacker, float delay, float pushBack, int AttackType)
    {
        this.damage = damage;
        this.attacker = attacker;
        this.delay = delay;
        this.pushBack = pushBack;
        this.AttackType = AttackType;
    }
}

public enum Target
{
    Player,
    Enemy,
    Bullet,
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
