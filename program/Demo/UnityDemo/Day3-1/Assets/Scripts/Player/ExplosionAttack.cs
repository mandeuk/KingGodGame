using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour {
    Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetTrigger("ChargeAttack");
            anim.SetBool("ChargeAttackB", true);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            anim.SetBool("ChargeAttackB",false);
        }
	}
}
