using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusHealth : Enemyhealth {
    CorosusBase corosusEntity;
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        corosusEntity = entity as CorosusBase;
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        base.TakeDamage(damageNode);
        EffectManager.instance.PlayEffect(gameObject, damageNode.AttackType, EffectManager.instance.playEnemyHitEffect);
        SoundManager.playEnemyStillHit();
        //if(!BossLifebarManager.instance)
        //    BossLifebarManager.instance.UpdateBosslifeGauge(entity.maxHP, entity.curHP);
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithWorriorDeadEffect);
        SoundManager.playCorosusDying();
        corosusEntity.Dead();
    }

    public override void Death()
    {
        BossLifebarManager.instance.InActivateBosslifeGauge();
        corosusEntity.HeadDestroy();
        base.Death();
    }
}
