using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTarHealth : Enemyhealth {

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
        EffectManager.instance.PlayEffect(gameObject, damageNode.AttackType, EffectManager.instance.playEnemyHitEffect);
        SoundManager.playEnemyNormalHit();
    }

    public override void Death()
    {
        base.Death();
        anim.speed = 0;
    }

    public void AfterAttackDeath()
    {
        anim.speed = 0;
        DeadEffect();
    }

    public override void DeadEffect()
    {
        base.DeadEffect();

        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyIceTarBulletHitEffect);
        SoundManager.playWraithDying();
    }
}
