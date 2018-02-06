using UnityEngine;
using System.Collections;

public class SlimeAttack : MonoBehaviour {
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 1;
    
    GameObject player;
    PlayerHealth playerHealth;
    SlimeHealth slimeHealth;
    bool playerInRange;
    float timer;

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        slimeHealth = GetComponent<SlimeHealth>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if(timer > timeBetweenAttacks && playerInRange && slimeHealth.currentHealth > 0)
        {
            Attack();
        }
	}

    void Attack()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
