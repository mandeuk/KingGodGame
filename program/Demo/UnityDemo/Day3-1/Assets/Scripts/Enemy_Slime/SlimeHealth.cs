using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;

    public float flashSpeed = 5f; // 맞았을때 번쩍이는 시간
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f); // 맞았을때 테투리가 빨강.

    public float sinkSpeed = 1f;

    private Material slimeMat;
    private Animator anim;
    private NavMeshAgent slimenavMesh;
    private Rigidbody slimerigidBody;

    bool isDead;
    public bool isSinking;
    bool damaged;

    public void StopMoving()
    {
        slimerigidBody.Sleep();
        slimenavMesh.speed = 0;
    }

    //public void PushBack(Vector3 playerPosition, float delay, float pushBack)
    //{
    //    Vector3 diff = playerPosition - transform.position;
    //    slimerigidBody.AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 5000f * pushBack);
    //    Invoke("StopMoving", .08f);
    //}

    //public void TakeDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
    //{
    //    damaged = true;
    //    PushBack(playerPosition, delay, pushBack);
    //    currentHealth -= damage;
    //    //print(currentHealth);

    //    if (currentHealth <= 0 && !isDead)
    //    {
    //        Death();
    //    }
    //}

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        try
        {
            slimenavMesh.speed = 0;
            anim.speed = 0;
            TakeDamage(damage);
            Vector3 diff = playerPosition - transform.position;
            GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 200f * pushBack);
            StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(stateNum));
            damaged = false;
        }
        catch (MissingComponentException e)
        {
            Debug.Log(e.ToString());

        }

        yield return new WaitForSeconds(.1f);
        slimerigidBody.Sleep();

        yield return new WaitForSeconds(delay);

        if (!isSinking)
        {
            slimenavMesh.speed = 1;
            anim.speed = 1;
        }
        yield break;

    }

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        slimenavMesh = GetComponent<NavMeshAgent>();
        slimerigidBody = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
        slimeMat = transform.GetChild(0).GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        if (damaged)
            slimeMat.SetColor("_Color", Color.black);

        else
            slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white, flashSpeed * Time.deltaTime));

        if (isSinking)            
            slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white * 40, .6f * Time.deltaTime));
    }

    void Death()
    {
        isDead = true;
      
        slimeMat.SetColor("_Color", Color.black * 0.2f);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        Destroy(gameObject, .8f);
    }
}
