﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBase : EnemyBase {
    public float bulletSpeed;

    protected override void Init()
    {
        base.Init();
        base.maxHP = 100;
        base.curHP = 100;
        base.moveSpeed = 2;

        base.attackPower = 1;
        base.attackDistance = 6;
        base.stopDistance = 5;
        base.findDistance = 10;
    }

	// Use this for initialization
	void Awake () {
        Init();
    }
	
	// Update is called once per frame
}
