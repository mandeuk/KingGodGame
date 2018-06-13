using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MoveBase {
    PlayerBase playerEntity;

    Animator avatar;
    public float turnSpeed;
    private float turnSpeedTime;
    public bool moveRoom = false;

    public Vector3 movePos;
    public Transform skillMovePos;
    //AfterImageEffect raphaelEX;

    int turnDir;
    float turnAngle = 17;

    protected override void Init()
    {
        base.Init();
        playerEntity = GetComponent<ObjectBase>() as PlayerBase;
        avatar = GetComponent<Animator>();
        turnSpeedTime = turnSpeed * Time.deltaTime;
        skillMovePos = GetComponentInChildren<SkillTarget>().transform;
        movePos = transform.forward;
    }

    void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (!moveRoom)
        {
            if (rigid /*&& !avatar.GetBool("StartAttack")*/) // 여기다가 키입력을 두니까 공격도중 속도가 안떨어짐 그래서 일일히넣음...
            {
                if (playerEntity.isDodge)
                    rigid.MovePosition(transform.position +
                        skillMovePos.forward * Time.deltaTime * avatar.GetFloat("DashForce") * PlayerBase.instance.moveSpeed);
                else if  (playerEntity.isExmove)
                    rigid.MovePosition(transform.position +
                        skillMovePos.forward * Time.deltaTime * PlayerBase.instance.moveSpeed);
                else
                    rigid.MovePosition(transform.position +
                        transform.forward * Time.deltaTime * avatar.GetFloat("DashForce") * PlayerBase.instance.moveSpeed);
                
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    if (!playerEntity.isAttack && !playerEntity.isExmove && !playerEntity.isChargeAttack && (avatar.GetFloat("DodgeTiming") < 0.1f))
                        TurnJudgeFunc();

                    if (!playerEntity.isExmove && !playerEntity.isChargeAttack && (avatar.GetFloat("DodgeTiming") < 0.1f))
                        Turn(movePos);
                }
                else
                    StopWalking();
            }
        }
        else
            StopWalking();
    }

    void Turn(Vector3 movePos)
    {
        turnDir = Turnjudge(transform.forward, movePos.normalized);
        if (Vector3.Angle(transform.forward, movePos.normalized) > turnAngle)
        {
            rigid.MoveRotation(rigid.rotation * Quaternion.Euler(new Vector3(0, turnDir * turnSpeedTime * 100, 0) * Time.deltaTime));
            playerEntity.isTurn = true;
        }

        else
        {
            rigid.rotation = Quaternion.LookRotation(movePos.normalized);
            IsWalking();
        }
    }

    public void TurnJudgeFunc()
    {
        if (Input.GetKey(KeyCode.A)) //Quaternion.LookRotation(new Vector3(-1, 0, 0))
            movePos.Set(-1, 0, 1);

        if (Input.GetKey(KeyCode.D))
            movePos.Set(1, 0, -1);

        if (Input.GetKey(KeyCode.W))
            movePos.Set(1, 0, 1);

        if (Input.GetKey(KeyCode.S))
            movePos.Set(-1, 0, -1);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            movePos.Set(0, 0, 1);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            movePos.Set(1, 0, 0);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            movePos.Set(-1, 0, 0);

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            movePos.Set(0, 0, -1);
    }

    void IsWalking()
    {
        avatar.SetBool("Dash", true);
        playerEntity.isMove = true;
    }

    void StopWalking()
    {
        avatar.SetBool("Dash", false);
        playerEntity.isMove = false;
    }

    public void DashEndForce()
    {
        //rigidbody.AddForce(transform.forward.normalized * 400);
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }
}
