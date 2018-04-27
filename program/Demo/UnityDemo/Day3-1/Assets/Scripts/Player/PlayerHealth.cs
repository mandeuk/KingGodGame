using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float startHealth;
    public float currentHealth;
    public AudioClip deathClip;

    Animator anim;
    PlayerMovement playerMovement;
    bool isDead;

	// Use this for initialization
	void Awake () {
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

    public void PlayerDamaged()
    {
        StopCoroutine(PlayerColorChange.instance.ColorChange());
        StartCoroutine(PlayerColorChange.instance.ColorChange());
    }
}
