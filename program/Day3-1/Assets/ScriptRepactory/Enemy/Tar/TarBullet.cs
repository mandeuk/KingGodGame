using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarBullet : BulletBase {
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<ObjectBase>().isDead && !other.GetComponent<ObjectBase>().isInvincibility)
                BulletHit();
        }

        if (other.CompareTag("mapClearCol"))
        {
            BulletHit();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void BulletHit()
    {
        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyTarBulletHitEffect);

        base.BulletHit();
    }

    public override IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(2.0f);
        BulletHit();

        yield break;
    }
}
