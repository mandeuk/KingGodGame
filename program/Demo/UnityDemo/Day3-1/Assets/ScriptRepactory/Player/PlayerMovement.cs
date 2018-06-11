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
    //AfterImageEffect raphaelEX;

    int turnDir;
    float turnAngle = 17;

    protected override void Init()
    {
        base.Init();
        playerEntity = entity as PlayerBase;
        avatar = GetComponent<Animator>();
        turnSpeedTime = turnSpeed * Time.deltaTime;

        movePos = new Vector3(1, 0, 0);
        
    }

    void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (!moveRoom)
        {
            if (rigid /*&& !avatar.GetBool("StartAttack")*/) // ����ٰ� Ű�Է��� �δϱ� ���ݵ��� �ӵ��� �ȶ����� �׷��� ����������...
            {
                if (!playerEntity.isExmove)
                    rigid.MovePosition(transform.position +
                        transform.forward * Time.deltaTime * avatar.GetFloat("DashForce") * PlayerBase.instance.moveSpeed);
                else
                    rigid.MovePosition(transform.position +
                            GetComponentInChildren<SkillTarget>().transform.forward * Time.deltaTime * PlayerBase.instance.moveSpeed);

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    if (!playerEntity.isAttack && !playerEntity.isExmove)
                        TurnJudgeFunc();

                    if (!playerEntity.isExmove)
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