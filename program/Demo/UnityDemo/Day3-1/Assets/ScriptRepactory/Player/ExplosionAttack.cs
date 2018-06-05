using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour {
    Animator anim;
    PlayerHealth isPlayerDeath;
    EXMove isPlayerEXMove;
    public GameObject noiseCamera;
    public bool isChargeAttack;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        isPlayerDeath = GetComponent<PlayerHealth>();
        isPlayerEXMove = GetComponent<EXMove>();
        isChargeAttack = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlayerEXMove.onEXMove && !isChargeAttack)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (PlayerStatus.instance.Energy > 0)
                {
                    anim.SetTrigger("ChargeAttack");
                    anim.SetBool("ChargeAttackB", true);
                }
            }

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
        PlayerStatus.instance.Energy -= 1;
        PlaySceneUIManager.instance.ChangeEnergyAmountText();
    }
}
