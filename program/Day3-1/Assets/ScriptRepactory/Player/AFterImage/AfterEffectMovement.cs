﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEffectMovement : MonoBehaviour {
    protected Animator avatar;
    public float turnSpeed;
    public float moveSpeed = 1;
    public bool turnPosible;

    Vector3 movePos;
    Rigidbody rigidBodyRaphael;
    PlayerBase playerEntity;

    void Awake()
    {
        rigidBodyRaphael = GetComponent<Rigidbody>();
        avatar = GetComponent<Animator>();
        playerEntity = PlayerBase.instance;
    }

    void FixedUpdate()
    {
        if (avatar)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if (!playerEntity.isExmove && !playerEntity.isDodge && !playerEntity.isAttack)
                {
                    TurnJudgeFunc();
                    Turn(movePos);
                }
            }
            else
                StopWalking();

            if (Input.GetKey(KeyCode.J))
            {
                avatar.SetBool("Combo", true);
            }
        }
    }

    void Turn(Vector3 movePos)
    {
        //turnDir = Turnjudge(transform.forward, movePos.normalized);
        //if (Vector3.Angle(transform.forward, movePos.normalized) > turnAngle)
        //    rigidBodyRaphael.MoveRotation(rigidBodyRaphael.rotation * Quaternion.Euler(new Vector3(0, turnDir * turnSpeedTime * 100, 0) * Time.deltaTime));

        //else
        {
            rigidBodyRaphael.rotation = Quaternion.LookRotation(movePos.normalized);
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
