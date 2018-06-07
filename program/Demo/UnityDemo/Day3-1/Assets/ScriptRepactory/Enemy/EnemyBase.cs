using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : ObjectBase {
    public float attackDistance, stopDistance, findDistance, turnSpeed;
    public bool isAgro;
    public GameObject player;

    protected override void Init()
    {
        base.Init();
        isAgro = false;
        player = PlayerBase.instance.gameObject;
        StartCoroutine(FindPlayer());
    }

    public override void Damaged(DamageNode damageNode)
    {
        GetComponent<Enemyhealth>().Damaged(damageNode);
    }

    public override void Dead()
    {

    }

    public override void Attack()
    {
        GetComponent<EnemyAttack>().NormalAttack();
    }

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
