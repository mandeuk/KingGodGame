using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    PlayerBase playerEntity;
    protected Animator avatar;

    //public NormalTarget normalTarget;
    //public SkillTarget skillTarget;
    //public ChargeAttackTarget chargeAttackTarget;

    public GameObject attackAtrea;
    public GameObject skillAttackArea;
    public GameObject chargeAttackArea;

    DamageNode skillDamageNode;
    DamageNode chargeDamageNode;


    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        playerEntity = GetComponent<ObjectBase>() as PlayerBase;
        avatar = GetComponent<Animator>();
    }

    protected virtual IEnumerator damageOn()
    {
        attackAtrea.SetActive(true);
        yield return 0;
        yield return 0; // 프레임 하나로 하니까 인식을 못하는경우가 있음. 그냥 두개로 할랜다.
        attackAtrea.SetActive(false);
        yield break;
    }

    protected IEnumerator skillDamageOn()
    {
        skillAttackArea.SetActive(true);
        yield return 0;
        yield return 0;
        skillAttackArea.SetActive(false);
        yield break;
    }

    protected IEnumerator chargeDamageOn()
    {
        chargeAttackArea.SetActive(true);
        yield return 0;
        yield return 0;
        chargeAttackArea.SetActive(false);
        yield break;
    }

    // Area들마다 트리거가 두갠데 지금 맨 위에있는 트리거에만 damageNode를 주는것같음
    // 이유는 enemyHealth에서 데미지노드로 체력을 깎는곳에서 버그가 나는데
    // 불렛은 어짜피 healthBase가 없으니까 에러가 나지 않는다.
    // 트리거의 순서로 이것만 바꿔주면 되는걸 보니 맨 위의 AttackTrigger만 불러온다는 소리.
    // 이걸 어떻게 할까. -> 해결 foreach문을 일단 쓰는거로....
    public override void NormalAttack()
    {
        damageNode.SetNode(entity.attackPower, entity.gameObject, 0.1f, entity.pushBack, (int)AttackType.Normal);
        //attackAtrea.GetComponent<AttackTrigger>().damageNode = damageNode;
        foreach (AttackTrigger trigger in attackAtrea.GetComponents<AttackTrigger>())
            trigger.damageNode = damageNode;
        StartCoroutine(damageOn());
    }

    public void SkillAttack()
    {
        damageNode.SetNode(entity.attackPower, entity.gameObject, 0.1f, entity.pushBack, (int)AttackType.SkillAttack);
        //skillAttackArea.GetComponent<AttackTrigger>().damageNode = damageNode;
        foreach (AttackTrigger trigger in skillAttackArea.GetComponents<AttackTrigger>())
            trigger.damageNode = damageNode;
        StartCoroutine(skillDamageOn());
    }

    public void ChargeAttack()
    {
        damageNode.SetNode(entity.attackPower, entity.gameObject, 0.1f, entity.pushBack, (int)AttackType.ChargeAttack);
        //chargeAttackArea.GetComponent<AttackTrigger>().damageNode = damageNode;
        foreach (AttackTrigger trigger in chargeAttackArea.GetComponents<AttackTrigger>())
            trigger.damageNode = damageNode;
        StartCoroutine(chargeDamageOn());
    }

    //public void NormalAttack(int stateNum)
    //{
    //    List<Collider> targetList = new List<Collider>(normalTarget.targetList);
    //    List<Collider> enemyBulletList = new List<Collider>(normalTarget.enemyBulletList);
    //    List<Collider> realTargetList = new List<Collider>();
    //    DamageNode damageNode =
    //        new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.2f, playerEntity.pushBack, stateNum);

    //    if (normalTarget.anotherTargetList.Count > 0)
    //    {
    //        foreach (Collider one in targetList)
    //        {
    //            if (normalTarget.anotherTargetList.Count > 0)
    //            {
    //                if (!one.GetComponent<Enemyhealth>().CheckBtwRapObj())
    //                {
    //                    realTargetList.Add(one);
    //                }
    //            }
    //        }

    //        if (realTargetList.Count > 0)
    //        {
    //            playerEntity.PlayerStiff();
    //        }

    //        foreach (Collider one in realTargetList)
    //        {
    //            ObjectBase enemy = one.GetComponent<ObjectBase>();
    //            if (enemy != null && !enemy.isDead)
    //            {
    //                enemy.Damaged(damageNode);
    //            }
    //        }
    //    }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

    //    else
    //    {
    //        if (targetList.Count > 0)
    //        {
    //            playerEntity.PlayerStiff();
    //        }

    //        foreach (Collider one in targetList)
    //        {
    //            ObjectBase enemy = one.GetComponent<ObjectBase>();
    //            if (enemy != null && !enemy.isDead)
    //            {
    //                enemy.Damaged(damageNode);
    //            }
    //        }
    //    }

    //    if(enemyBulletList.Count > 0)
    //    {
    //        foreach (Collider one in enemyBulletList)
    //        {
    //            one.GetComponent<BulletBase>().BulletHit();
    //        }
    //    }
    //}

    //public void skillAttack(int stateNum)
    //{
    //    List<Collider> targetList = new List<Collider>(skillTarget.targetList);
    //    List<Collider> realTargetList = new List<Collider>();
    //    DamageNode damageNode =
    //        new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.4f, playerEntity.pushBack + 7, 4);

    //    if (skillTarget.anotherTargetList.Count > 0)
    //    {
    //        foreach (Collider one in targetList)
    //        {
    //            if (skillTarget.anotherTargetList.Count > 0)
    //            {
    //                if (!one.GetComponent<Enemyhealth>().CheckBtwRapObj())
    //                {
    //                    realTargetList.Add(one);
    //                }
    //            }
    //        }

    //        if (realTargetList.Count > 0)
    //        {
    //            EventManager.CameraMoveEvent((int)CameraMoveType.EX);
    //        }

    //        foreach (Collider one in realTargetList)
    //        {
    //            EnemyBase enemy = one.GetComponent<ObjectBase>() as EnemyBase;
    //            if (enemy != null && !enemy.isDead)
    //            {
    //                enemy.SkillDamaged(damageNode);
    //            }
    //        }
    //    }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

    //    else
    //    {
    //        if (targetList.Count > 0)
    //        {
    //            EventManager.CameraMoveEvent((int)CameraMoveType.EX);
    //            //StartCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(targetList.Count * 0.02f));
    //        }

    //        foreach (Collider one in targetList)
    //        {
    //            EnemyBase enemy = one.GetComponent<ObjectBase>() as EnemyBase;
    //            if (enemy != null && !enemy.isDead)
    //            {
    //                enemy.SkillDamaged(damageNode);
    //                //StartCoroutine(enemy.GetComponent<HealthBase>().SkillDamaged(damageNode));
    //            }
    //        }
    //    }
    //}

    //public void ChargeAttack()
    //{
    //    List<Collider> targetList = new List<Collider>(chargeAttackTarget.targetList);
    //    List<Collider> enemyBulletList = new List<Collider>(chargeAttackTarget.enemyBulletList);
    //    DamageNode damageNode =
    //        new DamageNode(playerEntity.attackPower * 5, playerEntity.gameObject, 0.2f, playerEntity.pushBack + 5, 4);

    //    foreach (Collider one in targetList)
    //    {
    //        ObjectBase enemy = one.GetComponent<ObjectBase>();
    //        if (enemy != null && !enemy.isDead)
    //        {
    //            StartCoroutine(enemy.GetComponent<HealthBase>().NormalDamaged(damageNode));
    //        }
    //    }

    //    if (enemyBulletList.Count > 0)
    //    {
    //        foreach (Collider one in enemyBulletList)
    //        {
    //            one.GetComponent<BulletBase>().BulletHit();
    //        }
    //    }
    //}

    void Update()
    {
        if (!playerEntity.isExmove && !playerEntity.isChargeAttack)
        {
            if (Input.GetKey(KeyCode.J))
            {
                OnAttacking();
            }
        }
    }

    public virtual void OnAttacking()
    {
        avatar.SetBool("Combo", true);
    }

    public virtual void StopAttacking()
    {
        avatar.SetBool("Combo", false);
    }

    // 애니메이션에서 타이밍 맞게 불러주는 함수.
    public virtual void NormalAttackEvent(int stateNum)
    {
        if (playerEntity.isAttack)  // 중간에 ex무브나 다른 상태로 빠지면 발동안되게끔..
        {
            EffectManager.instance.PlayEffect(gameObject, stateNum, EffectManager.instance.playPlayerAttackEffect);
            //NormalAttack(stateNum);
            NormalAttack();
            
            // 공격 이벤트 콜
            EventManager.AttackEvent(stateNum);
        }
    }
}
