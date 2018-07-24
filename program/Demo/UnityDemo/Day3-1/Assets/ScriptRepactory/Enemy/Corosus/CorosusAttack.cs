using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusAttack : RangeAttack {
    protected CorosusBase Corosus;
    protected int attackCnt;

    // Use this for initialization
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        InitBullet();
        base.Init();
        Corosus = GetComponent<ObjectBase>() as CorosusBase;
    }

    // Update is called once per frame
    void Update()
    {
        AttackUpdate();
    }

    protected override void AttackUpdate()
    {
        if (enemyEntity.isAgro)
        {
            if (Corosus.isMove)
                Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < enemyEntity.attackDistance && Corosus.isAttackReady)
            {
                StartAttack();
            }
        }
    }

    public override void StartAttack()
    {
        Corosus.isAttackReady = false;

        if (Random.Range(1, 4) > 2)
        {
            StartCoroutine(BulletAttack());
        }
        else
        {
            StartCoroutine(NormalFire());
        }
    }

    public override void StopAttack()
    {        
        StopCoroutine(NormalFire());
    }

    public void TurnAndFire()
    {
        int turnDir;

        Vector3 forwardPos = transform.position + transform.forward;

        Vector3 forwardVec = forwardPos - transform.position;
        Vector3 diff = player.transform.position - transform.position;

        turnDir = Turnjudge(forwardVec.normalized, diff.normalized);

        if (Vector3.Angle(forwardVec.normalized, diff.normalized) > 16.0f)
        {
            enemyEntity.isTurn = true;
            transform.Rotate(new Vector3(0, turnDir * 1 * 100, 0) * Time.deltaTime);            
        }

        else
        {
            transform.LookAt(player.transform.position);
            anim.SetBool("Attack", true);
            enemyEntity.isTurn = false;
        }
    }

    public override void Fire()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = bulletList[0];
            bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
            bullet.GetComponent<BulletBase>().Attacker = gameObject;

            firedBulletList.Add(bullet);
            bulletList.RemoveAt(0);
            bullet.SetActive(true);

            bullet.transform.position = transform.position + transform.up + transform.forward + (transform.right * (i - 2)) * 0.7f;
            bullet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 40 + i * 20.0f, 0);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 400);
        }
        attackCnt += 1;
    }

    public override void InitBullet()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject bulletclone = Instantiate(bullet, cloneSpawnObject.transform) as GameObject;

            bulletList.Add(bulletclone);
        }
    }

    public IEnumerator NormalFire()
    {        
        Corosus.isAttackReady = false;
        attackCnt = 0;

        yield return new WaitForSeconds(2.0f);
        while (attackCnt < 3)
        {
            TurnAndFire();
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("Attack", false);

        yield return new WaitForSeconds(2.0f);
        anim.SetBool("Attack", false);
        Corosus.isAttackReady = true;
        yield break;
    }

    public IEnumerator BulletAttack()
    {
        int angle = 0;
        Corosus.isAttackReady = false;
        Corosus.isAttack = true;
        yield return new WaitForSeconds(2.0f);

        while (angle < 250)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject bullet = bulletList[0];
                bullet.GetComponent<AttackTrigger>().damageNode = damageNode;
                bullet.GetComponent<BulletBase>().Attacker = gameObject;

                firedBulletList.Add(bullet);
                bulletList.RemoveAt(0);
                bullet.SetActive(true);

                bullet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + i * 90.0f + angle, 0);
                bullet.transform.position = transform.position + transform.up + bullet.transform.forward;
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 180);
            }
            yield return new WaitForSeconds(.1f);
            angle += 10;
        }

        yield return new WaitForSeconds(2.0f);
        Corosus.isAttackReady = true;
        Corosus.isAttack = false;
        yield break;
    }
}
