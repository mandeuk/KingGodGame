﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhealth : HealthBase
{
    public float flashSpeed = 1f;

    protected EnemyBase enemyEntity;
    protected GameObject player;
    protected Animator anim;

    protected Material[] enemyMat;
    protected Color[] enemyMatOrigColor;

    // Use this for initialization
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        enemyEntity = entity as EnemyBase;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        enemyMat = new Material[GetComponentsInChildren<Renderer>().Length];
        enemyMatOrigColor = new Color[GetComponentsInChildren<Renderer>().Length];

        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            enemyMat[i] = GetComponentsInChildren<Renderer>()[i].material;
            enemyMatOrigColor[i] = enemyMat[i].color;
        }
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        entity.curHP -= damageNode.damage;
        EventManager.EnemyHitEvent(damageNode.AttackType, this.gameObject);
        base.TakeDamage(damageNode);

        if (!(damageNode.AttackType == 5))
        {
            anim.SetTrigger("Damaged" + Random.Range(1, 3));
        }

        if (!enemyEntity.isDead)
        {
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", Color.black);
            }
        }
    }
    public override void AfterDamage(DamageNode damageNode)
    {
        rigid.Sleep();
        base.AfterDamage(damageNode);
        StartCoroutine(ColorChange());
    }

    // 죽는건 EnemyHealth에서 처리합니다.
    public override void Death()
    {
        enemyEntity.isDead = true;
        anim.speed = 0.5f;
       
        StartCoroutine(ColorChangeDie());

        Invoke("DeadEffect", 0.8f);
        Destroy(gameObject, .8f);
    }

    // 함수 기능 :  결국 그냥 구현함
    //public override IEnumerator NormalDamaged(DamageNode damageNode)
    //{
    //    TakeDamage(damageNode);
    //    yield return new WaitForSeconds(damageNode.delay);
    //    if (!enemyEntity.isDead)
    //    {
    //        rigid.Sleep();
    //        StartCoroutine(ColorChange());
    //    }
    //    enemyEntity.isDamaged = false;
    //    yield break;
    //}

    public override IEnumerator SkillDamaged(DamageNode damageNode)
    {
        Vector3 diff = damageNode.attacker.transform.position - transform.position;

        EventManager.EnemyHitEvent(damageNode.AttackType, this.gameObject);
        enemyEntity.isDamaged = true;
        anim.SetTrigger("Damaged" + Random.Range(1, 3));
        rigid.Sleep();
        anim.speed = 0;

        yield return new WaitForSeconds(.5f);
        anim.speed = 1;
        rigid.AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 400f * damageNode.pushBack);
        entity.curHP -= damageNode.damage;
        
        if (!enemyEntity.isDead)
        {
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", new Vector4(.2f, .2f, .2f, 1));
            }
        }

        if (entity.curHP <= 0 && !entity.isDead)
        {
            Death();
            yield break;
        }

        yield return new WaitForSeconds(damageNode.delay);
        if (!enemyEntity.isDead)
        {
            AfterDamage(damageNode);
        }

        yield break;
    }

    // 함수 기능 :  죽었을 때 에너지, 에테르를 드랍하고 이펙트를 만듬.
    public virtual void DeadEffect()
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            EnergyManager.instance.DropEtere(transform.position);
        }
        if(Random.Range(1,100) > 85)
            EnergyManager.instance.DropEnergy(transform.position);
    }

    // 적과 플레이어 사이에 장애물이 있는지 체크함.
    public bool CheckBtwRapObj()
    {
        Vector3 diff = player.transform.position - transform.position;
        float dist = Vector3.Distance(player.transform.position, transform.position);

        RaycastHit[] colList = Physics.RaycastAll(transform.position, new Vector3(diff.x, 0f, diff.z).normalized, dist);

        for (int i = 0; i < colList.Length; i++)
        {
            if (colList[i].transform.CompareTag("Obstacle"))
            {
                return true;
            }
        }
        return false;
    }

    // 함수 기능 :  원래 색으로 돌려주는 기능.
    public IEnumerator ColorChange()
    {
        float timer = new float();
        while (timer < 3)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", Color.Lerp(enemyMat[i].GetColor("_Color"), enemyMatOrigColor[i], flashSpeed * Time.deltaTime));
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    // 함수 기능 :  흰색으로 변하는 기능.
    public IEnumerator ColorChangeDie()
    {
        StopCoroutine(ColorChange());
        float timer = new float();
        while (timer < 3)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
            {
                enemyMat[i].SetColor("_Color", Color.Lerp(enemyMat[i].GetColor("_Color"), Color.white * 10, Time.deltaTime));
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}