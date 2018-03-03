using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    protected Animator avatar;
    public float turnSpeed;
    public float moveSpeed = 1;
    private float turnSpeedTime;

    Vector3 movePos;
    Rigidbody rigidbody;

    int turnDir;
    float turnAngle = 15;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        avatar = GetComponent<Animator>();
        turnSpeedTime = turnSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (avatar)
        {
            if (rigidbody /*&& !avatar.GetBool("StartAttack")*/) // 여기다가 키입력을 두니까 공격도중 속도가 안떨어짐 그래서 일일히넣음...
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.A)) //Quaternion.LookRotation(new Vector3(-1, 0, 0))
                        movePos.Set(-1, 0, 1);

                    else if (Input.GetKey(KeyCode.D))
                        movePos.Set(1, 0, -1);

                    else if(Input.GetKey(KeyCode.W))
                        movePos.Set(1, 0, 1);

                    else if(Input.GetKey(KeyCode.S))
                        movePos.Set(-1, 0, 1);

                    else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                        movePos.Set(0, 0, 1);

                    else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                        movePos.Set(1, 0, 0);

                    else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                        movePos.Set(-1, 0, 0);

                    else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                        movePos.Set(0, 0, -1);

                    if (!avatar.GetBool("StartAttack")) // 공격중이 아니며 키를 누르고 있을때.
                    {
                        Dash(movePos);
                    }
                }

                else
                    StopWalking();

                if (Input.GetKeyDown(KeyCode.K))
                {
                    avatar.SetTrigger("Attack");
                }

            }
        }

    }


    void Dash(Vector3 movePos)
    {
        Vector3 movePos1 = movePos.normalized * moveSpeed * Time.deltaTime;

        turnDir = Turnjudge(transform.forward, movePos);
        if (Vector3.Angle(transform.forward, movePos) > turnAngle)
        {
            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
        }

        else
        {
            transform.rotation = Quaternion.LookRotation(movePos.normalized);
            rigidbody.MovePosition(transform.position + movePos1);
            IsWalking();
        }
    }


    void IsWalking()
    {
        avatar.SetBool("Dash", true);
    }

    void StopWalking()
    {
        avatar.SetBool("Dash", false);
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }
}
