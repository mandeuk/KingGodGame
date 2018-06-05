using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack {
    public GameObject attackAtrea;
     
	void Awake () {
        Init();
    }

	// 업데이트도 상속받아서 쓰고싶지만 모노비헤이비어에서 제공하는 업데이트라서
    // 새로 new도 못하고 오버라이딩도 힘듬..
    // 할수는 있겠지만 지금은 어려워서 구현 할 수 없음.
	void Update () {
        if (enemyEntity.isAgro)
        {
            if (!enemyEntity.isAttack)
                base.Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < enemyEntity.attackDistance
                && !enemyEntity.isTurn)
            {
                base.StartAttack();
            }
            else
            {
                base.StopAttack();
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        attackAtrea.GetComponent<AttackTrigger>().damageNode = damageNode;
    }

    public override void NormalAttack()
    {
        attackAtrea.SetActive(true);
        attackAtrea.SetActive(false);
    }
}
