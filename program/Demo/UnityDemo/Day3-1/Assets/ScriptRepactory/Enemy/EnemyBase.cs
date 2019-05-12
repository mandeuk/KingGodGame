using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade
{
    Normal,
    Rare,
    Epic,
    Legendary
}

public class EnemyBase : ObjectBase {
    public float attackDistance, stopDistance, findDistance, turnSpeed;
    public bool isAgro;
    public GameObject player;
    public Grade grade;

    protected override void Init()
    {
        base.Init();
        grade = Grade.Normal;
        isAgro = false;
        player = PlayerBase.instance.gameObject;
        StartCoroutine(FindPlayer());
    }

    public override void Attack()
    {
        GetComponent<EnemyAttack>().NormalAttack();
    }

    //public virtual void SkillDamaged(DamageNode damageNode)
    //{
    //    GetComponent<Enemyhealth>().SkillDamaged(damageNode);
    //}

    public virtual void OnAgro()
    {        
        if (Vector3.Distance(player.transform.position, transform.position) > findDistance)
        {
            return;
        }

        else
        {
            isAgro = true;
        }
    }

    public IEnumerator FindPlayer()
    {
        while (!isAgro)
        {
            OnAgro();
            yield return null;
        }        
        yield break;
    }
}
