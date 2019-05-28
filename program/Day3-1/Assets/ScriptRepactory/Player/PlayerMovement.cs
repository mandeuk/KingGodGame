using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MoveBase {
    PlayerBase playerEntity;

    Animator avatar;
    float turnSpeed;
    public bool moveRoom = false;

    public Vector3 movePos;
    public Transform skillMovePos;

    int turnDir;
    float turnAngle = 17;

    protected override void Init()
    {
        base.Init();
        playerEntity = GetComponent<ObjectBase>() as PlayerBase;
        avatar = GetComponent<Animator>();
        movePos = transform.forward;
        rigid.rotation = Quaternion.LookRotation(Vector3.right);
        transform.rotation = Quaternion.LookRotation(Vector3.right);
        turnSpeed = 10;
    }

    void Awake()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (!moveRoom)  // �̰� ���߿� playerDisable �̰� �Լ��� ��¥�� �����ϱ� ����ϰ� ��������ϼ�.
        {
            if (rigid /*&& !avatar.GetBool("StartAttack")*/) // ����ٰ� Ű�Է��� �δϱ� ���ݵ��� �ӵ��� �ȶ����� �׷��� ����������...
            {
                if (playerEntity.isDodge)
                    rigid.MovePosition(transform.position +
                        skillMovePos.forward * Time.deltaTime * avatar.GetFloat("DashForce") * playerEntity.moveSpeed);
                else if  (playerEntity.isExmove)
                    rigid.MovePosition(transform.position +
                        skillMovePos.forward * Time.deltaTime * playerEntity.moveSpeed);
                else
                    rigid.MovePosition(transform.position +
                        transform.forward * Time.deltaTime * avatar.GetFloat("DashForce") * playerEntity.moveSpeed);
                
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    if (!playerEntity.isAttack && !playerEntity.isExmove && !playerEntity.isChargeAttack && (avatar.GetFloat("DodgeTiming") < 0.1f))
                        TurnJudgeFunc();

                    if (!playerEntity.isExmove /*&& !playerEntity.isChargeAttack*/
                        && (avatar.GetFloat("DodgeTiming") < 0.1f))
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
            rigid.MoveRotation(rigid.rotation * Quaternion.Euler(new Vector3(0, turnDir * turnSpeed * 100, 0) * Time.deltaTime));
            playerEntity.isTurn = true;
        }

        else
        {
            rigid.rotation = Quaternion.LookRotation(movePos.normalized);
            playerEntity.isTurn = false;
            IsWalking();
        }
    }

    public void TurnJudgeFunc()
    {
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
        //movePos.Set(x, 0, y);

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
        if (playerEntity.isFly)
            avatar.SetBool("Dash", true);
        else
            avatar.SetBool("Run", true);
        playerEntity.isMove = true;
    }

    void StopWalking()
    {
        if (playerEntity.isFly)
            avatar.SetBool("Dash", false);
        else
            avatar.SetBool("Run", false);
        playerEntity.isMove = false;
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }
}
