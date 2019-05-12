using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBossMovement : EnemyMovement {

    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    private void FixedUpdate()
    {
        base.MoveUpdate();
    }
}
