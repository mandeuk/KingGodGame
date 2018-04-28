using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour {
    PlayerEffect playerEffect;
    Animator anim;

	// Use this for initialization
	void Awake () {
        playerEffect = GetComponent<PlayerEffect>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //playerEffect.playExplosionAttackEffect();
            anim.SetTrigger("ChargeAttack");
        }
	}
}
