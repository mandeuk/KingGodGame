using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyAttack {
    public GameObject bullet;
    public GameObject cloneSpawnObject;
    [SerializeField]
    List<GameObject> bulletList = new List<GameObject>();
    [SerializeField]
    List<GameObject> firedBulletList = new List<GameObject>();

    protected override void Init()
    {
        base.Init();

        InitBullet();
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
        bullet.transform.position = transform.transform.position + transform.up * 0.5f;
        bullet.transform.rotation = transform.transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 80);
    }

    public void BulletHit(GameObject hitBullet)
    {
        hitBullet.GetComponent<Rigidbody>().Sleep();
        hitBullet.SetActive(false);
        bulletList.Add(hitBullet);
        firedBulletList.Remove(hitBullet);
    }

    public void InitBullet()
    {
        bulletList = EffectManager.instance.enemyWraithBullets;
        firedBulletList = EffectManager.instance.enemyUsedWraithBullets;
        //cloneSpawnObject = GameObject.FindWithTag("ClonesObject");
        //for (int i = 0; i < 3; i++)
        //{
        //    GameObject bulletClone = Instantiate(bullet, cloneSpawnObject.transform) as GameObject;
        //    bulletClone.GetComponent<AttackTrigger>().damageNode = damageNode;
        //    bulletClone.GetComponent<BulletBase>().Attacker = gameObject;
        //    bulletList.Add(bulletClone);
        //}
    }
}
