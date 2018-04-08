using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour {
    PlayerEffect playerEffect;

	// Use this for initialization
	void Awake () {
        playerEffect = transform.GetComponent<PlayerEffect>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerEffect.playExplosionAttackEffect();
        }
	}
}
