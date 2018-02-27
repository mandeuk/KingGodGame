using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    protected Animator avatar;
    public float turnSpeed;
    public float moveSpeed = 1;
    private float turnSpeedTime;
    int turnDir;

    void Start()
    {
        avatar = GetComponent<Animator>();
        turnSpeedTime = turnSpeed * Time.deltaTime;
    }

    float h, v;
    float turnAngle = 15;
    
    void Update()
    {
        if (avatar)
        {
            avatar.SetFloat("Speed", (v * v));

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody /*&& !avatar.GetBool("StartAttack")*/) // 여기다가 키입력을 두니까 공격도중 속도가 안떨어짐 그래서 일일히넣음...
            {
                
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.A) && !avatar.GetBool("StartAttack")) //Quaternion.LookRotation(new Vector3(-1, 0, 0))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(-1, 0, 1));
                        if (Vector3.Angle(transform.forward, new Vector3(-1, 0, 1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);

                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));
                            rigidbody.AddForce(new Vector3(-1, 0, 1) * 200 * moveSpeed);
                            //transform.Translate(new Vector3(-2, 0, 2) * moveSpeed * Time.deltaTime,Space.World);
                            IsWalking();
                        }

                    }

                    if (Input.GetKey(KeyCode.D) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(1, 0, -1));
                        if (Vector3.Angle(transform.forward, new Vector3(1, 0, -1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, -1));
                            rigidbody.AddForce(new Vector3(1, 0, -1) * 200 * moveSpeed);
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.W) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(1, 0, 1));
                        if (Vector3.Angle(transform.forward, new Vector3(1, 0, 1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
                            rigidbody.AddForce(new Vector3(1, 0, 1) * 200 * moveSpeed);
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.S) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(-1, 0, -1));
                        if (Vector3.Angle(transform.forward, new Vector3(-1, 0, -1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, -1));
                            rigidbody.AddForce(new Vector3(-1, 0, -1) * 200 * moveSpeed);
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(0, 0, 1));
                        if (Vector3.Angle(transform.forward, new Vector3(0, 0, 1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
                            rigidbody.AddForce(new Vector3(0, 0, 1) * 200 * moveSpeed * (float)Math.Sqrt(2));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(1, 0, 0));
                        if (Vector3.Angle(transform.forward, new Vector3(1, 0, 0)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                            rigidbody.AddForce(new Vector3(1, 0, 0) * 200 * moveSpeed *(float)Math.Sqrt(2));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(-1, 0, 0));
                        if (Vector3.Angle(transform.forward, new Vector3(-1, 0, 0)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
                            rigidbody.AddForce(new Vector3(-1, 0, 0) * 200 * moveSpeed * (float)Math.Sqrt(2));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(0, 0, -1));
                        if (Vector3.Angle(transform.forward, new Vector3(0, 0, -1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
                            rigidbody.AddForce(new Vector3(0, 0, -1) * 200 * moveSpeed * (float)Math.Sqrt(2));
                            IsWalking();
                        }
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

    void IsWalking()
    {
        v = 1;
        avatar.SetBool("Dash", true);
    }

    void StopWalking()
    {
        v = 0;
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
