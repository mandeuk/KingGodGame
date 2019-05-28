using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBossAttack : RangeAttack {
    protected WraithBossBase wraithBoss;
    protected int attackCnt;

    // Use this for initialization
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {        
        base.Init();
        turnSpeed = 1.5f;
        wraithBoss = entity as WraithBossBase;
    }

    // Update is called once per frame
    void Update()
    {
        AttackUpdate();
    }

    protected override void AttackUpdate()
    {
        if (enemyEntity.isAgro && !enemyEntity.isDead)
        {
            // 레이스는 플레이어를 바라보지 않아도 되는 탄막공격이 존재하기때문에 turn이 움직일때만 되면 됨.
            if (wraithBoss.isMove)
                Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < enemyEntity.attackDistance && wraithBoss.isAttackReady)
            {
                StartAttack();
            }
        }
    }

    public override void StartAttack()
    {
        wraithBoss.isAttackReady = false;

        if (Random.Range(1, 4) > 0)
        {
            StartCoroutine(NormalFire());            
        }
        else
        {
            StartCoroutine(BulletAttack());
        }
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
            transform.Rotate(new Vector3(0, turnDir * 1 * 100, 0) * Time.deltaTime * turnSpeed);
        }

        else
        {
            transform.LookAt(player.transform.position);
            anim.SetBool("Attack", true);
            enemyEntity.isTurn = false;
        }
    }

    // 애니메이션FBX 타이밍 맞춰서 부르는 노말어택에서 Fire가 불림.
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
        for (int i = 0; i < 40; i++)
        {
            GameObject bulletclone = Instantiate(bullet) as GameObject;

            bulletList.Add(bulletclone);
        }
    }

    public IEnumerator NormalFire()
    {
        wraithBoss.isAttackReady = false;
        attackCnt = 0;

        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        while (attackCnt < 3)
        {
            TurnAndFire();
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("Attack", false);

        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        anim.SetBool("Attack", false);
        wraithBoss.isAttackReady = true;
        yield break;
    }

    public IEnumerator BulletAttack()
    {
        int angle = 0;
        wraithBoss.isAttackReady = false;
        wraithBoss.isAttack = true;
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));

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
            yield return new WaitForSeconds(.15f);
            angle += 10;
        }

        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        wraithBoss.isAttackReady = true;
        wraithBoss.isAttack = false;
        yield break;
    }
}
