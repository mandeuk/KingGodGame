using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBullet : BulletBase {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!other.GetComponent<ObjectBase>().isDead && !other.GetComponent<ObjectBase>().isInvincibility)
                BulletHit();
        }

        if (other.CompareTag("mapClearCol"))
        {
            BulletHit();
        }
    }

    public override void BulletHit()
    {
        onFire = false;

        EffectManager.instance.PlayEffect(gameObject, 1, EffectManager.instance.playEnemyWraithBulletHitEffect);
        Attacker.GetComponent<RangeAttack>().BulletHit(gameObject);
    }
}
