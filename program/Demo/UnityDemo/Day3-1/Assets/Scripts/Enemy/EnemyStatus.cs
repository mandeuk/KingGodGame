using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {
    private static EnemyStatus instance = null;

    public float wraithHealthPoint { set; get; }
    public float wraithAttackPower { set; get; }
    public float wraithAttackRange { set; get; }
    public float wraithMoveSpeed { set; get; }
    public float wraithAngleSpeed { set; get; }

    // Use this for initialization
    void Awake()
    {
        instance = this;

        wraithHealthPoint = 100;
        wraithAttackPower = 10;
        wraithAttackRange = 7;
        wraithMoveSpeed = 2;
        wraithAngleSpeed = 200;

    }
}
