using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageAttack : PlayerAttack {
    AfterImageBase afterimagebase;

    protected override void Init()
    {
        base.Init();
        afterimagebase = GetComponent<ObjectBase>() as AfterImageBase;
        avatar = GetComponent<Animator>();
    }

    // Use this for initialization
    void Awake () {
        Init();
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            OnAttacking();
        }
    }

    public override void OnAttacking()
    {
        avatar.SetBool("Combo", true);
    }

    public override void StopAttacking()
    {
        avatar.SetBool("Combo", false);
    }

    public override void NormalAttackEvent(int stateNum)
    {

    }
}
