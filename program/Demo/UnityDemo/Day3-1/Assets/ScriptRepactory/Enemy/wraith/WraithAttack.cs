using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAttack : RangeAttack {

	// Use this for initialization
	void Awake () {
        Init();
    }

    protected override void Init()
    {
        //InitBullet();   // 레이스는 레이스의 불렛을 이닛해야하기때문에 얘가 따로 해줌.
                        // 자신의총알을 채워야 하기때문에 base.init()도 불러줌.
        base.Init();
    }

    // Update is called once per frame
    void Update () {
        base.AttackUpdate();
    }

    public override void Fire()
    {
        GameObject bullet = bulletList[0];
        bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
        bullet.GetComponent<BulletBase>().Attacker = gameObject;

        firedBulletList.Add(bullet);
        bulletList.RemoveAt(0);
        bullet.SetActive(true);

        bullet.transform.position = transform.transform.position + transform.up * 0.5f;
        bullet.transform.rotation = transform.transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * 4000);
        SoundManager.playWraithAttack();
    }

    //public override GameObject SpawnBullet()
    //{
    //    GameObject bulletClone = Instantiate(Resources.Load("Prefabs/Enemy/WraithAttackBall"), transform) as GameObject;

    //    return bulletClone;
    //}
}
