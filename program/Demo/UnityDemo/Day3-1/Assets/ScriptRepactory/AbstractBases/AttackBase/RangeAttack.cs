using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyAttack {
    public GameObject bullet;
    [SerializeField]
    List<GameObject> bulletList = new List<GameObject>();
    [SerializeField]
    List<GameObject> firedBulletList = new List<GameObject>();


    // Use this for initialization
    void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void Init()
    {
        base.Init();
        InitBullet();
    }

    public override void NormalAttack()
    {
        Fire();
    }

    public void Fire()
    {
        GameObject bullet = bulletList[0];

        firedBulletList.Add(bullet);
        bulletList.RemoveAt(0);

        bullet.SetActive(true);
        bullet.transform.position = transform.transform.position + transform.up * 0.5f;
        bullet.transform.rotation = transform.transform.rotation;
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
        for(int i = 0; i < 3; i++)
        {
            GameObject bulletClone = Instantiate(bullet, transform) as GameObject;
            bulletClone.GetComponent<AttackTrigger>().damageNode = damageNode;
            bulletList.Add(bulletClone);
        }
    }
}
