using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyssBullet : BulletBase {

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    if (!other.GetComponent<ObjectBase>().isDead && !other.GetComponent<ObjectBase>().isInvincibility)
        //        BulletHit();
        //}

        //if (other.CompareTag("mapClearCol"))
        //{
        //    BulletHit();
        //}
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
    
    public override void BulletHit()
    {
        onFire = false;

        Attacker.GetComponent<LotusOfAbyssPlayer>().BulletHit(gameObject);
    }
}
