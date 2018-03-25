using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public static float healthPoint;
    public static float Energy;
    public static float attackPower;
    public static float attackSpeed;
    public float moveSpeed;
    public static float attackRange;
    public static float devilGage;

    public static float Ether;

    public static float gameTime;
    public static float pushBackPower;

    // 게터 셋터 전부 만들어서 쓰기
    // 다른 cs파일에서 수정하는것을 한번에 관리할 수 있게끔.

    private void Awake()
    {

    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
