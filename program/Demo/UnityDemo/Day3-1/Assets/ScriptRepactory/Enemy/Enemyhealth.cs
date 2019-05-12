using System.Collections;
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

    public override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Init()
    {
        base.Init();
        enemyEntity = entity as EnemyBase;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        enemyMat = new Material[GetComponentsInChildren<SkinnedMeshRenderer>().Length];
        enemyMatOrigColor = new Color[GetComponentsInChildren<SkinnedMeshRenderer>().Length];

        for (int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer>().Length; i++)
        {
            enemyMat[i] = GetComponentsInChildren<SkinnedMeshRenderer>()[i].material;
            enemyMatOrigColor[i] = enemyMat[i].color;
        }
    }

    public override void Damaged(DamageNode damageNode)
    {
        if (damageNode.AttackType != (int)AttackType.SkillAttack || enemyEntity.grade == Grade.Legendary)
        {
            base.Damaged(damageNode);
        }
        else
        {
            SkillDamaged(damageNode);
        }
    }

    public override void TakeDamage(DamageNode damageNode)
    {
        entity.curHP -= damageNode.damage;
        EventManager.EnemyHitEvent(damageNode.AttackType, this.gameObject);
        EventManager.CameraMoveEvent((int)CameraMoveType.Normal);
        base.TakeDamage(damageNode);
        
        // attackType 이 5는 soulofimp 아이템임.. 바꿔야됨
        // 애당초 공격에 맞는모션을 실행시키는 공격인지 아닌지가 있어야할듯.
        if (!(damageNode.AttackType == 5))
        {
            anim.SetTrigger("Damaged" + Random.Range(1, 3));
        }

        if (!enemyEntity.isDead)
        {
            for (int i = 0; i < enemyMat.Length; i++)
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
    
    public virtual void SkillDamaged(DamageNode damageNode)
    {
        if (/*!entity.isDamaged &&*/ !entity.isInvincibility)
        {
            StartCoroutine(SkillDamagedRoutine(damageNode));
        }
    }

    IEnumerator SkillDamagedRoutine(DamageNode damageNode)
    {
        if (CheckBtwRapObj())
            yield break;

        Vector3 diff = damageNode.attacker.transform.position - transform.position;        
        enemyEntity.isDamaged = true;
        rigid.Sleep();
        anim.speed = 0;

        yield return new WaitForSeconds(1f);
        anim.speed = 1;
        TakeDamage(damageNode);
        rigid.Sleep();
        rigid.AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 200f * damageNode.pushBack);

        yield return new WaitForSeconds(1f);
        AfterDamage(damageNode);

        yield break;
    }

    // 죽는건 EnemyHealth에서 처리합니다.
    public override void Death()
    {
        base.Death();
        anim.speed = 0.35f;

        StartCoroutine(ColorChangeDie());

        Invoke("DeadEffect", 0.8f);
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

        //entity.Dead();  // 이거 나중에 바꿔야됨 순서가 잘못됨. 엔티티에서 dead가 불리는식으로 해야할듯..? 아닌가..
        // setActive(false) 도 여기서해줌.
        gameObject.SetActive(false);
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
            for (int i = 0; i < enemyMat.Length; i++)
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
            for (int i = 0; i < enemyMat.Length; i++)
            {
                enemyMat[i].SetColor("_Color", Color.Lerp(enemyMat[i].GetColor("_Color"), Color.white * 10, Time.deltaTime * 1f));
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}