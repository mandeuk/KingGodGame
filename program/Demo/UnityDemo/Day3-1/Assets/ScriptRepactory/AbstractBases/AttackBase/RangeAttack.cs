using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyAttack {
    public GameObject bullet;
    public GameObject cloneSpawnObject;
    [SerializeField]
    protected List<GameObject> bulletList = new List<GameObject>();

    [SerializeField]
    protected List<GameObject> firedBulletList = new List<GameObject>();

    protected override void Init()
    {
        base.Init();
    }

    public override void NormalAttack()
    {        
        Fire();
    }

    public virtual void Fire()
    {
        GameObject bullet = bulletList[0];
        bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
        bullet.GetComponent<BulletBase>().Attacker = gameObject;

        firedBulletList.Add(bullet);
        bulletList.RemoveAt(0);
        bullet.SetActive(true);
    }

    public virtual void BulletHit(GameObject hitBullet)
    {
        hitBullet.GetComponent<Rigidbody>().Sleep();
        hitBullet.SetActive(false);
        bulletList.Add(hitBullet);
        firedBulletList.Remove(hitBullet);
    }

    public virtual void InitBullet()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject bulletclone = Instantiate(bullet, cloneSpawnObject.transform) as GameObject;

            bulletList.Add(bulletclone);
        }
    }
}
