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
            //if(!other.GetComponent<ObjectBase>().isInvincibility)
                BulletHit();
        }
    }

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(VanishingEffect());
        StartCoroutine(Move());
        Init();
    }

    public override void Init()
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
            if (!target)
            {
                BulletHit();
                yield break;
            }

            dir = (target.transform.position - transform.position).normalized;
            rigid.MovePosition(transform.position + dir * 25 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
