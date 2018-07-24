using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBossBullet : BulletBase {

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

    public override void Init()
    {
        base.Init();
    }
}
