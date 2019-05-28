using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackBase {
    protected EnemyBase enemyEntity;
    protected GameObject player;
    protected Animator anim;
    protected GameObject Target;
    protected float turnSpeed;

    protected override void Init()
    { 
        base.Init();
        turnSpeed = 2.5f;
        enemyEntity = entity as EnemyBase;
        player = enemyEntity.player;
        anim = GetComponent<Animator>();
    }

    protected virtual void AttackUpdate()
    {
        if (enemyEntity.isAgro && !enemyEntity.isDead)
        {
            // turn 관련해서 AI좀 바꿔야할듯.
            // 바라보고 움직이고, 바라보고 공격하고를 해야해서
            // nav와 관련된 movement와 attack 알고리즘 다시설계해야함.
            if (!enemyEntity.isAttack)
                Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < enemyEntity.attackDistance
                && !enemyEntity.isTurn)
            {
                StartAttack();
            }
            else
            {
                StopAttack();
            }
        }
    }
    
    void Start () {
        Init();
    }

    public virtual void StartAttack()
    {
        anim.SetBool("Attack", true);
    }

    public virtual void StopAttack()
    {
        anim.SetBool("Attack", false);
    }

    public virtual void Turn()
    {
        int turnDir;
        
        Vector3 forwardPos = transform.position + transform.forward;

        Vector3 forwardVec = forwardPos - transform.position;
        Vector3 diff = player.transform.position - transform.position;

        turnDir = Turnjudge(forwardVec.normalized, diff.normalized);

        if (Vector3.Angle(forwardVec.normalized, diff.normalized) > 16.0f)
        {
            enemyEntity.isTurn = true;
            if (!enemyEntity.isAttack)
            {
                transform.Rotate(new Vector3(0, turnDir * 1 * 100, 0) * Time.deltaTime * turnSpeed);
            }
        }

        else
        {
            transform.LookAt(player.transform.position);
            enemyEntity.isTurn = false;
        }
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }

    public override void NormalAttack()
    {

    }
}
