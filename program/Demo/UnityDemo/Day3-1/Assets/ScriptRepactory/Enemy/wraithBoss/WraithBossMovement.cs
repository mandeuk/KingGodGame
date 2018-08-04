using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBossMovement : EnemyMovement {

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
        base.MoveUpdate();
    }
}
