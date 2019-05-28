using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMovement : EnemyMovement
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }
    
    void Update()
    {
        base.MoveUpdate();
    }
}
