using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBullet : BulletBase {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("mapClearCol"))
        {
            BulletHit();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
        Init();
    }

    // 함수 기능 :  5초뒤에 사라지게 함.
    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(5.0f);
        BulletHit();

        yield break;
    }

    public override void Init()
    {
        base.Init();
    }

    public override void BulletHit()
    {
        base.BulletHit();

        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithBulletHitEffect);
    }
}
