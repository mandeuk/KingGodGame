using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public static int healthPoint;
    public static int Energy;
    public static int attackPower;
    public static int attackSpeed;
    public static int moveSpeed;
    public static int attackRange;
    public static int devilGage;

    public static int Ether;

    private void Awake()
    {
        healthPoint = 6;
        Ether = 1;
        attackPower = 3;
        attackSpeed = 10;
        moveSpeed = 3;
        attackRange = 10;
        devilGage = 300;

        Ether = 0;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
