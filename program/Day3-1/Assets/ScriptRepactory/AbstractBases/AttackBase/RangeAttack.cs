using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyAttack {
    public GameObject bullet;

    [SerializeField]
    protected List<GameObject> bulletList = new List<GameObject>();

    [SerializeField]
    protected List<GameObject> firedBulletList = new List<GameObject>();

    protected override void Init()
    {
        base.Init();
        InitBullet();
    }

    public override void NormalAttack()
    {        
        Fire();
    }

    // 애니메이션FBX 타이밍 맞춰서 부르는 노말어택에서 Fire가 불림.
    public virtual void Fire()
    {
        //GameObject bullet = bulletList[0];
        //bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
        //bullet.GetComponent<BulletBase>().Attacker = gameObject;

        //firedBulletList.Add(bullet);
        //bulletList.RemoveAt(0);
        //bullet.SetActive(true);
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
        for (int i = 0; i < 10; i++)
        {
            //GameObject bulletclone = Instantiate(SpawnBullet()) as GameObject;
            GameObject bulletclone = Instantiate(bullet) as GameObject; 

            bulletList.Add(bulletclone);
        }
    }
    
    //public virtual GameObject SpawnBullet()
    //{
    //    //GameObject bulletClone = Instantiate(Resources.Load("Prefabs/Effect/WraithBulletAttackHit")) as GameObject;
    //    //
    //    //return bulletClone;

    //    GameObject bulletClone = new GameObject();

    //    return bulletClone;
    //}
}
