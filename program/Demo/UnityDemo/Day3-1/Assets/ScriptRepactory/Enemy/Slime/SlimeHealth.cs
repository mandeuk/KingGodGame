﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeHealth : MonoBehaviour {
    public float startingHealth = 100;
    public float currentHealth;
    public GameObject player;

    public float flashSpeed = 5f; // 맞았을때 번쩍이는 시간

    Vector4 originColor;

    private Material slimeMat;
    private Animator anim;
    private Rigidbody slimeRigidBody;

    bool isDead = false;
    public bool isSinking = false;
    bool damaged = false;
    bool isEXMove = false;

    public bool CheckBtwRapObj()
    {
        Vector3 diff = player.transform.position - transform.position;
        float dist = Vector3.Distance(player.transform.position, transform.position);

        RaycastHit[] colList = Physics.RaycastAll(transform.position, new Vector3(diff.x, 0f, diff.z).normalized, dist);

        for (int i = 0; i < colList.Length; i++) {
            if (colList[i].transform.CompareTag("Obstacle"))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(float amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public IEnumerator StartSkillDamage(float damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        damaged = true;
        isEXMove = true;
        slimeMat.SetColor("_Color", new Vector4(.1f, .1f, .1f, 1));
        //anim.speed = 0;
        Vector3 diff = playerPosition - transform.position;

        yield return new WaitForSeconds(0.4f);
        
        isEXMove = false;
        

        yield return new WaitForSeconds(.2f);
        TakeDamage(damage);
        damaged = false;
        GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 560f * pushBack);
        StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(5,this.gameObject));

        yield return new WaitForSeconds(.2f);
        slimeRigidBody.Sleep();

        yield return new WaitForSeconds(delay);

        yield break;
    }

    public IEnumerator StartDamage(float damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        Time.timeScale = .5f;
        slimeMat.SetColor("_Color", new Vector4(.2f, .2f, .2f, 1));
        //anim.speed = 0;
        TakeDamage(damage);
        Vector3 diff = playerPosition - transform.position;
        GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 400f * pushBack);
        StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(stateNum,this.gameObject));

        //while (timef + 0.5f > Time.time)
        //{
        //    yield return new WaitForSeconds(0.05f);
        //}

        yield return new WaitForSeconds(.1f);
        slimeRigidBody.Sleep();
        damaged = false;
        Time.timeScale = 1;

        yield return new WaitForSeconds(delay);
        if (!isSinking)
        {
            //anim.speed = 1;
        }
        yield break;

    }

    private void Awake()
    {
        //anim = transform.GetChild(0).GetComponent<Animator>();
        slimeRigidBody = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
        slimeMat = transform.GetChild(0).GetComponent<Renderer>().material;
        originColor = slimeMat.color;
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (!damaged && !isSinking)
        {
            //slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_Color"), flashSpeed * Time.deltaTime));
            slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), originColor, flashSpeed * Time.deltaTime));
        }

        //if (!isEXMove)
        //{
        //    slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white, flashSpeed * Time.deltaTime));
        //}

        if (isEXMove)
        {
            slimeRigidBody.Sleep();
        }

        if (isSinking)            
            slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white * 3, Time.deltaTime));
    }

    void Death()
    {
        isDead = true;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        

        //몬스터사망 후 아이템 생성
        //ItemManager.Instance.SpawnItem(gameObject.transform.position);

        Destroy(gameObject, .8f);
    }
}