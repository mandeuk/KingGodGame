using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    PlayerStatus raphaelStatus;

    protected Animator avatar;
    public float turnSpeed;
    private float turnSpeedTime;
    public bool turnPosible;

    Vector3 movePos;
    Rigidbody rigidbody;
    PlayerAttack playerattack;
    //AfterImageEffect raphaelEX;

    int turnDir;
    float turnAngle = 15;

    void Awake()
    {
        raphaelStatus = GetComponent<PlayerStatus>();
        rigidbody = GetComponent<Rigidbody>();
        avatar = GetComponent<Animator>();
        playerattack = GetComponent<PlayerAttack>();
        turnSpeedTime = turnSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (avatar)
        {
            if (rigidbody /*&& !avatar.GetBool("StartAttack")*/) // 여기다가 키입력을 두니까 공격도중 속도가 안떨어짐 그래서 일일히넣음...
            {
                if (!transform.GetComponent<EXMove>().onEXMove)
                    rigidbody.MovePosition(transform.position +
                        transform.forward * Time.deltaTime * avatar.GetFloat("DashForce") * raphaelStatus.moveSpeed);
                else
                    rigidbody.MovePosition(transform.position +
                            GetComponentInChildren<SkillTarget>().transform.forward * Time.deltaTime * raphaelStatus.moveSpeed);

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    if (!playerattack.b_attacking && !transform.GetComponent<EXMove>().onEXMove)
                        TurnJudgeFunc();
                    
                    Turn(movePos);
                }
                else
                    StopWalking();
            }
        }
    }

    void Turn(Vector3 movePos)
    {
        //Vector3 movePos1 = movePos.normalized * moveSpeed * Time.deltaTime;

        turnDir = Turnjudge(transform.forward, movePos.normalized);
        if (Vector3.Angle(transform.forward, movePos.normalized) > turnAngle)
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, turnDir * turnSpeedTime * 100, 0) * Time.deltaTime));
        else
        {
            rigidbody.rotation = Quaternion.LookRotation(movePos.normalized);
            turnPosible = false;
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
    }

    void StopWalking()
    {
        avatar.SetBool("Dash", false);
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
