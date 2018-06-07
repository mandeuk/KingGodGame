using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour {
    Animator anim;
    EXMove isPlayerEXMove;
    public GameObject noiseCamera;
    PlayerBase playerEntity;

	// Use this for initialization
	void Awake () {
        playerEntity = PlayerBase.instance;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!playerEntity.isExmove && !playerEntity.isChargeAttack)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (PlayerStatus.instance.Energy > 0)
                {
                    anim.SetTrigger("ChargeAttack");
                    anim.SetBool("ChargeAttackB", true);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            anim.SetBool("ChargeAttackB", false);
        }
    }

    public void ChargeAttackCameraEvent(float time)
    {
        EXMoveCam.instance.playEXCameraEvent(time);
    }

    public void UseEnergy()
    {
        PlayerStatus.instance.Energy -= 1;
        PlaySceneUIManager.instance.ChangeEnergyAmountText();
    }
}
