using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusAttack : MeleeAttack {
    CorosusBase corosusEntity;
    public GameObject dashAttackAtrea;
    // Use this for initialization
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        corosusEntity = entity as CorosusBase;
    }

    private void FixedUpdate()
    {
        AttackUpdate();
    }

    public void DashAttack()
    {
        dashAttackAtrea.GetComponent<AttackTrigger>().damageNode = damageNode;
        dashAttackAtrea.GetComponent<CorosusAttackTrigger>().attacker = this.gameObject;

        //  코로서스 이펙트 넣어야됨.
        //EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithWorriorAttackEffect);
    }

    //  점프어택 땅에 찍을때 발동될거임
    public override void NormalAttack()
    {
        //  코로서스 이펙트 넣어야됨.
    }

    protected override void AttackUpdate()
    {
        if (enemyEntity.isAgro)
        {
            if (!enemyEntity.isAttack && !corosusEntity.isDealTime)
                Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < enemyEntity.attackDistance
                && !enemyEntity.isTurn && corosusEntity.isAttackReady)
            {
                StartAttack();
            }
        }
    }

    public override void StartAttack()
    {
        corosusEntity.isAttackReady = false;
        if (Random.Range(1, 5) < 3)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            JumpAttack();
        }
    }

    public void Dash()
    {
        attackAtrea.SetActive(true);
        StartCoroutine(Dashing());
    }

    public void JumpAttack()
    {
        anim.SetBool("JumpAttack", true);
        StartCoroutine(JumpAttacking());
    }

    IEnumerator JumpAttacking()
    {
        corosusEntity.isDealTime = true;

        yield return new WaitForSecondsRealtime(Random.Range(3.5f, 6.5f));
        corosusEntity.isDealTime = false;
        corosusEntity.isAttackReady = true;
        yield break;
    }

    IEnumerator Dashing()
    {
        float timer = new float();
               
        while (timer < 7.0f && !corosusEntity.isBreak)
        {
            timer += Time.fixedDeltaTime;
            rigid.MovePosition(transform.position + transform.forward * Time.deltaTime * corosusEntity.dashSpeed);

            yield return new WaitForEndOfFrame();
        }
        attackAtrea.SetActive(false);
        anim.SetTrigger("Break");
        corosusEntity.isDealTime = true;

        yield return new WaitForSecondsRealtime(Random.Range(3.5f,5.5f));
        corosusEntity.isDealTime = false;
        anim.SetTrigger("Stun");
        corosusEntity.isBreak = false;
        corosusEntity.isAttackReady = true;
        yield break;
    }
}
