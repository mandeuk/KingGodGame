﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    protected Animator avatar;
    PlayerEffect effect;
    float f_last_attacktime;
    public bool b_attacking;

    public static int normalDamage = 10;
    public int skillDamage = 30;

    public NormalTarget normalTarget;

    public void NormalAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);

        foreach(Collider one in targetList)
        {
            SlimeHealth slime = one.GetComponent<SlimeHealth>();
            if (slime != null && !slime.isSinking)
            {
                StartCoroutine(slime.StartDamage(normalDamage, transform.position, 0.5f, 3f, stateNum));
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        avatar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("Player"))
            if (Input.GetKey(KeyCode.K))
                OnAttacking();
    }

    public void OnAttacking()
    {
        avatar.SetBool("Combo", true);
        //avatar.SetBool("StartAttack",true);
    }

    public void StopAttacking()
    {
        avatar.SetBool("Combo", false);
        //avatar.SetBool("StartAttack", false);
    }

    public void NormalAttackEvent(int stateNum)
    {
        if (transform.CompareTag("Player"))
        {
            StartCoroutine(transform.GetComponent<PlayerEffect>().PlayEffect(stateNum));
            NormalAttack(stateNum);
        }
    }
}
