using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackBase {
    protected EnemyBase enemyEntity;
    protected GameObject player;
    protected Animator anim;
    protected GameObject Target;

    protected override void Init()
    {
        base.Init();
        enemyEntity = GetComponent<ObjectBase>() as EnemyBase;
        player = enemyEntity.player;
        anim = GetComponent<Animator>();
    }

    public override void NormalAttack()
    {

    }

    // Use this for initialization
    void Start () {
        Init();
    }

    public void StartAttack()
    {
        entity.isAttack = true;
        anim.SetBool("Attack", true);
    }

    public void StopAttack()
    {
        entity.isAttack = false;
        anim.SetBool("Attack", false);
    }

    public void Turn()
    {
        int turnDir;
        
        Vector3 forwardPos = transform.position + transform.forward;

        Vector3 forwardVec = forwardPos - transform.position;
        Vector3 diff = player.transform.position - transform.position;

        turnDir = Turnjudge(forwardVec.normalized, diff.normalized);

        if (Vector3.Angle(forwardVec.normalized, diff.normalized) > 10.0f)
        {
            enemyEntity.isTurn = true;
            if (!enemyEntity.isAttack)
            {
                transform.Rotate(new Vector3(0, turnDir * 1 * 100, 0) * Time.deltaTime * 2);
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
}
