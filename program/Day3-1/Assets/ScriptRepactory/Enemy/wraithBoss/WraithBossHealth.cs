using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBossHealth : Enemyhealth {

    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void Death()
    {
        enemyEntity.isDead = true;
        anim.SetTrigger("Damaged" + Random.Range(1, 3));
        anim.speed = 0.5f;

        
        StartCoroutine(ColorChangeDie());

        Invoke("DeadEffect", 0.8f);
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        entity.curHP -= damageNode.damage;
        if (entity.curHP <= 0 && !entity.isDead)
        {
            Death();
        }
        EventManager.EnemyHitEvent(damageNode.AttackType, this.gameObject);
        EventManager.CameraMoveEvent((int)CameraMoveType.Normal);
        //Vector3 diff = damageNode.attacker.transform.position - transform.position;
        //rigid.AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 400f * damageNode.pushBack);
        //base.TakeDamage(damageNode);
        //anim.SetTrigger("Damaged" + Random.Range(1, 3));

        if (!enemyEntity.isDead)
        {
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", new Vector4(.2f, .2f, .2f, 1));
            }
        }

        EffectManager.instance.PlayEffect(gameObject, damageNode.AttackType, EffectManager.instance.playEnemyHitEffect);
        SoundManager.playEnemyNormalHit();

        //UI갱신하는 코드
        BossLifebarManager.instance.UpdateBosslifeGauge(entity.maxHP, entity.curHP);
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        
        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithWorriorDeadEffect);
        SoundManager.playWraithDying();

        //UI꺼주는 코드
        BossLifebarManager.instance.InActivateBosslifeGauge();
    }
}
