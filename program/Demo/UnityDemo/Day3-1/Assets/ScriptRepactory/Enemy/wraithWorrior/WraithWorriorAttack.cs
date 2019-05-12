﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithWorriorAttack : MeleeAttack {

    // Use this for initialization
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.AttackUpdate();
    }

    public override void NormalAttack()
    {
        base.NormalAttack();
        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithWorriorAttackEffect);
        SoundManager.playWraithWorriorAttack();
    }
}
