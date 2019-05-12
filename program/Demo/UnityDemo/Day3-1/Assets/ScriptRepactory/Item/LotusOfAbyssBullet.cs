using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyssBullet : BulletBase {

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
        onFire = false;

        Attacker.GetComponent<LotusOfAbyssPlayer>().BulletHit(gameObject);
    }

    public override IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(2.0f);
        BulletHit();

        yield break;
    }
}
