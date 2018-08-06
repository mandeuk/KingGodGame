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
        InitBullet();
        base.Init();        
    }

    // Update is called once per frame
    void Update () {
        base.AttackUpdate();
    }

    public override void Fire()
    {
        base.Fire();

        bullet.transform.position = transform.transform.position + transform.up * 0.5f;
        bullet.transform.rotation = transform.transform.rotation;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * 2500);
    }

    public override void InitBullet()
    {

        bulletList = EffectManager.instance.enemyWraithBullets;
        firedBulletList = EffectManager.instance.enemyUsedWraithBullets;
    }
}
