using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTarAttack : RangeAttack {
    IceTarBase tarEntity;

    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        tarEntity = entity as IceTarBase;
    }

    // Update is called once per frame
    void Update()
    {
        base.AttackUpdate();
    }

    public override void NormalAttack()
    {
        Fire();
        tarEntity.AfterAttackDead();
    }

    public override void Fire()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = bulletList[0];
            bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
            bullet.GetComponent<BulletBase>().Attacker = gameObject;

            firedBulletList.Add(bullet);
            bulletList.RemoveAt(0);
            bullet.SetActive(true);

            bullet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + i * 45.0f, 0);
            bullet.transform.position = transform.position + transform.up * 0.2f + bullet.transform.forward;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 150);
        }
    }
}
