using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public static PlayerHealth instance = null;
    public float startHealth;
    public float currentHealth;


    Animator anim;
    PlayerMovement playerMovement;
    public bool isDead;
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
            //anim.SetTrigger("Damage");
        }
    }

    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");
        transform.GetComponent<PlayerMovement>().enabled = false;
        transform.GetComponent<PlayerHealth>().enabled = false;
        transform.GetComponent<PlayerAttack>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Collider>().isTrigger = true;

        Invoke("loadScene", 5);
    }

    void loadScene()
    {
        SceneManager.LoadScene("Game_Junghoon");
    }

    public void PlayerDamaged(GameObject Hit)
    {
        if (!invincibilty && !isDead)
        {
            StopCoroutine(PlayerColorChange.instance.ColorChange());
            StartCoroutine(PlayerColorChange.instance.ColorChange());
            if (!superArmor)
            {
                GetComponent<Rigidbody>().AddForce(Hit.transform.forward * 6000);
            }
            TakeDamage(1);
            print("맞음");
        }
    }
}
