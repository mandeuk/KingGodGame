using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithWorriorHealth : Enemyhealth {
    // Use this for initialization
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

    public override void DeadEffect()
    {
        base.DeadEffect();

        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithWorriorDeadEffect);
        SoundManager.playWraithDying();
    }
}
