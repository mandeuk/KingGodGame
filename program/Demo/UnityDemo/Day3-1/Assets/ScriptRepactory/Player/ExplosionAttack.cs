using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : AttackBase {
    Animator anim;
    EXMove isPlayerEXMove;
    public GameObject noiseCamera;
    PlayerBase playerEntity;

	// Use this for initialization
	void Awake () {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        playerEntity = GetComponent<ObjectBase>() as PlayerBase;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (!playerEntity.isExmove && !playerEntity.isChargeAttack && !playerEntity.isDodge)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (PlayerBase.instance.energy > 0)
                {
                    playerEntity.isChargeAttack = true;
                    anim.SetTrigger("ChargeAttack");
                    anim.SetBool("ChargeAttackB", true);
                }

                else
                {
                    anim.SetTrigger("Dodge");
                }
            }
        }

        if (playerEntity.isChargeAttack && !playerEntity.isDodge)
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                anim.SetBool("ChargeAttackB", false);
            }
        }
    }

    public void ChargeAttackCameraEvent(float time)
    {
        EXMoveCam.instance.playEXCameraEvent(time);
    }

    public void UseEnergy()
    {
        PlayerBase.instance.energy -= 1;
        PlaySceneUIManager.instance.ChangeEnergyAmountText();
    }

    public override void NormalAttack()
    {

    }
}
