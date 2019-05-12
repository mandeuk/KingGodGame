using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestHealth : Enemyhealth {

    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        base.TakeDamage(damageNode);
        GameObject highpriestClone = new GameObject();
        highpriestClone.transform.position = gameObject.transform.position + Vector3.up * 1.5f;
        EffectManager.instance.PlayEffect(highpriestClone, damageNode.AttackType, EffectManager.instance.playEnemyHitEffect);
        SoundManager.playEnemyNormalHit();
        BossLifebarManager.instance.UpdateBosslifeGauge(entity.maxHP, entity.curHP);
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
    }

    public override void Death()
    {
        BossLifebarManager.instance.InActivateBosslifeGauge();
        base.Death();
    }
}
