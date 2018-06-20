using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WraithWorriorMovement : EnemyMovement {

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
