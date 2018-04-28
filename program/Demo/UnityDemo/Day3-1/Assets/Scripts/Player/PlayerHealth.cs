using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public static PlayerHealth instance = null;
    public float startHealth;
    public float currentHealth;


    Animator anim;
    PlayerMovement playerMovement;
    bool isDead;
    public bool superArmor;
    public bool invincibilty;

	// Use this for initialization
	void Awake () {
        instance = this;
        superArmor = false;
        invincibilty = false;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        startHealth = PlayerStatus.instance.healthPoint;
        currentHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
        else
        {
            anim.SetTrigger("Damage");
        }
    }

    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");
        playerMovement.enabled = false;
    }

    public void PlayerDamaged(GameObject Hit)
    {
        if (!invincibilty)
        {
            StopCoroutine(PlayerColorChange.instance.ColorChange());
            StartCoroutine(PlayerColorChange.instance.ColorChange());
            if (!superArmor)
            {
                GetComponent<Rigidbody>().AddForce(Hit.transform.forward * 6000);
            }
            //TakeDamage(1);
            print("맞음");
        }
    }
}
