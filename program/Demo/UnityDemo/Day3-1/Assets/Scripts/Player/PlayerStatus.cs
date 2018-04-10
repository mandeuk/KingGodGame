using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public static PlayerStatus instance;

    public float healthPoint    { set; get; }
    public float Energy         { set; get; }
    public float attackPower    { set; get; }
    public float attackSpeed    { set; get; }
    public float attackRange    { set; get; }
    public float devilGage      { set; get; }
    public float moveSpeed      { get; set; }
    public float Ether          { set; get; }

    public float gameTime       { set; get; }
    public float pushBackPower  { set; get; }

    // 게터 셋터 전부 만들어서 쓰기
    // 다른 cs파일에서 수정하는것을 한번에 관리할 수 있게끔.

    private void Awake()
    {
        instance = this;

        healthPoint = 3;
        Energy = 10;
        attackPower = 10;
        attackSpeed = 1;
        attackRange = 1;
        devilGage = 30;
        moveSpeed = 8;
        Ether = 10;
    }
}
