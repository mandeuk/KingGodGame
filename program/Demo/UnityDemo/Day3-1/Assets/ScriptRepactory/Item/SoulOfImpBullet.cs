using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulOfImpBullet : BulletBase {
    public GameObject target;
    Rigidbody rigid;
    Vector3 dir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            BulletHit();
            SoundManager.playSoulOfImpHit();
        }
    }

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(VanishingEffect());
        StartCoroutine(Move());
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void BulletHit()
    {
        gameObject.SetActive(false);
        onFire = false;
        StopCoroutine(Move());
        Attacker.GetComponent<SoulOfImp>().BulletHit(gameObject);
        EffectManager.instance.PlayEffect(gameObject, 5, EffectManager.instance.playEnemyImpHitEffect);
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            if (!target || target.GetComponent<ObjectBase>().isDead)
            {
                BulletHit();
                yield break;
            }

            //if(target.CompareTag("BossEnemy"))
            //{
            dir = (target.transform.GetChild(0).transform.position + Vector3.up * 0.3f - transform.position).normalized;
            //    print(target.transform.GetChild(0));
            //}
            //else
            //{
            //    dir = (target.transform.position - transform.position).normalized;
            //}
            
            rigid.MovePosition(transform.position + dir * 25 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }        
    }
}
