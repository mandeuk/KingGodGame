using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------//
//
//  FBX와 animator에 많이 의존하는 코드.
//  적절한 타이밍을 위해 FBX나 Animator에서 불러주는것이 좋다고 생각
//  그로인해 업데이트에서는 공격 조건에 맞으면 anim.setBool로 애니메이션을 재생시켜주고
//  그일이 끝날때까지 대기함. SetBool로 True가 된 animator parameter들은 animatoin behaviour에서 다 꺼줌.
//  이 알고리즘을 바탕으로 보스제작
//  나중에 보고 안까먹기 위해 써둠.
//
//-----------------------------------------------------------//

public class HighPriestAttack : RangeAttack {
    HighPriestBase priestEntity;
    GameObject Laser;
    int attackCnt;
    
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        InitLaser();
        turnSpeed = 1.5f;
        priestEntity = entity as HighPriestBase;
    }

    private void FixedUpdate()
    {
        AttackUpdate();
    }

    protected override void AttackUpdate()
    {
        if (enemyEntity.isAgro && !enemyEntity.isDead)
        {
            // 하이프리스트는 바라보고 하는 공격이 레이저랑 노말공격, 안바라봐도 되는 공격은 탄막공격
            // 그러면 노말공격과 레이저공격은 바라보고 쏘면 되기때문에 turnAndFire을 쓰면되고
            // 탄막공격은 안봐도되니까 Turn은 레이스보스와 마찬가지로 움직일때만 작동하면됨

            if (entity.isMove)
                Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < enemyEntity.attackDistance 
                && priestEntity.isAttackReady)
            {

                StartAttack();
            }

        }
    }

    // 공격이 준비가되면 startAttack();
    public override void StartAttack()
    {
        priestEntity.isAttackReady = false;
        StartCoroutine(AttackWaitTime());
    }

    // 공격이 시작되면 앞의 딜타임을 위해 공격을 조금 기다렸다가
    // 바라보는 공격(노말공격,레이저공격)을할지 제자리공격(와일드어택)을 할지 정함
    // 일반 공격들은 바로 setbool을 하지 않고 일단 turn을 하는 코루틴을 실행시킴
    // 와일드어택은 FBX 에 wildAttack()을 부르는 함수를 넣어둠
    IEnumerator AttackWaitTime()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        if (Random.Range(1, 10) > 4 || attackCnt < 3)
        {
            StartCoroutine(NormalFire());
        }
        else
        {
            anim.SetBool("WildAttack", true);
        }
        yield break;
    }

    public IEnumerator NormalFire()
    {
        while (!entity.isAttack)
        {
            TurnAndFire();
            yield return new WaitForEndOfFrame();
        }

        // 노말어택은 1.3초, 레이저어택은 총 3초
        yield return new WaitForSeconds(Random.Range(3.0f, 4.5f));
        priestEntity.isAttackReady = true;
        attackCnt++;
        yield break;
    }

    // wildAttackFBX에서 부르는 함수.
    public void WildAttack()
    {
        StartCoroutine(WildAttackCo());
    }

    public IEnumerator WildAttackCo()
    {
        int angle = 0;
        yield return new WaitForSeconds(1.4f);
        SoundManager.playHighPriestWildAttack();
        while (angle > -250)
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
                bullet.transform.position = transform.position + transform.up * 1.6f + bullet.transform.forward;
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 270);                
            }
            SoundManager.playHighPriestNormalAttack();
            yield return new WaitForSeconds(.15f);
            angle -= 15;
        }

        yield return new WaitForSeconds(Random.Range(3.5f, 4.5f));
        attackCnt = 0;
        priestEntity.isAttackReady = true;
        yield break;
    }

    // 돌고나서 무슨애니메이션을 재생할건지 결정하는 함수.
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

        // 다 돌고나면 레이저나 일반공격중 하나로 공격
        else
        {
            transform.LookAt(player.transform.position);
            if (Random.Range(1, 10) > 3)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("LaserAttack", true);
            }
            enemyEntity.isTurn = false;
        }
    }

    // laserAttack()에서 부르는 함수 오타남;
    public void LazerAttack()
    {
        Laser.GetComponent<HighPriestLaserObj>().damageNode = damageNode;
        Laser.transform.position = new Vector3(transform.position.x, Laser.transform.position.y, transform.position.z);
        Laser.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Laser.SetActive(true);
        Laser.GetComponent<HighPriestLaserObj>().LaserShot();

        // 레이저 프리펩 생성.
    }

    // normalAttack 에서 부르는 함수
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

            bullet.transform.position = transform.position + transform.up * 1.6f + transform.forward + (transform.right * (i - 2)) * 0.7f;
            bullet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 40 + i * 20.0f, 0);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 400);
        }
        SoundManager.playHighPriestNormalAttack();
    }

    public override void InitBullet()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject bulletclone = Instantiate(bullet) as GameObject;

            bulletList.Add(bulletclone);
        }
    }

    public void InitLaser()
    {
        Laser = Instantiate(Resources.Load("Prefabs/Enemy/HighPriestLaser")) as GameObject;
    }
}
