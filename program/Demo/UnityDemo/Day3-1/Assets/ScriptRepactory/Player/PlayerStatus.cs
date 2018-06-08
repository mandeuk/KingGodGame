using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public static PlayerStatus instance = null;

    public float maxHP          { set; get; }
    public float healthPoint    { set; get; }
    public float Energy         { set; get; }
    public float attackPower    { set; get; }
    public float attackSpeed    { set; get; }
    public float attackRange    { set; get; }
    public float devilGage      { set; get; }
    public float moveSpeed      { set; get; }
    public float etere          { set; get; }

    public float gameTime       { set; get; }
    public float pushBackPower  { set; get; }

    // 게터 셋터 전부 만들어서 쓰기
    // 다른 cs파일에서 수정하는것을 한번에 관리할 수 있게끔.

    private void Awake()
    {
        if (instance)//인스턴스가 생성되어있는가?
        {
            DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }
        
        maxHP = 5;
        healthPoint = 5;
        Energy = 3;
        attackPower = 30;
        attackSpeed = 1;
        attackRange = 1;
        devilGage = 30;
        moveSpeed = 8;
        etere = 0;
    }

    private void Start()
    {
        //StartCoroutine(PlayerActSceneStart());
    }

    public void PlayerEnable()
    {
        transform.GetComponent<PlayerMovement>().enabled = true;
        transform.GetComponent<PlayerHealth>().enabled = true;
        transform.GetComponent<PlayerAttack>().enabled = true;
        //transform.GetComponent<EXMove>().enabled = true;
        transform.GetComponent<ExplosionAttack>().enabled = true;
        //transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Collider>().isTrigger = false;
    }
    
    public void PlayerDisable()
    {
        transform.GetComponent<PlayerMovement>().enabled = false;
        transform.GetComponent<PlayerHealth>().enabled = false;
        transform.GetComponent<PlayerAttack>().enabled = false;
        //transform.GetComponent<EXMove>().enabled = false;
        transform.GetComponent<ExplosionAttack>().enabled = false;
        //transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Collider>().isTrigger = true;
    }

    public IEnumerator PlayerActSceneStart()
    {
        PlayerColorChange.instance.PlayerDisappear();

        yield return new WaitForSeconds(2f);
        PlayerColorChange.instance.PlayerAppear();
        GetComponent<PlayerEffect>().playEXmoveVanishFlowerEffect();
        GetComponent<Animator>().SetTrigger("roomMove");

        yield break;
    }
}
