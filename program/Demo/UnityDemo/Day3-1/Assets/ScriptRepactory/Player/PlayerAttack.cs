using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    PlayerBase playerEntity;
    public GameObject normalAttacCam;
    public GameObject exMoveCam;
    protected Animator avatar;

    public NormalTarget normalTarget;
    public SkillTarget skillTarget;
    public ChargeAttackTarget chargeAttackTarget;

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

    public void NormalAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);
        List<Collider> enemyBulletList = new List<Collider>(normalTarget.enemyBulletList);
        List<Collider> realTargetList = new List<Collider>();
        DamageNode damageNode =
            new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.2f, playerEntity.pushBack, stateNum);

        if (normalTarget.anotherTargetList.Count > 0)
        {
            foreach (Collider one in targetList)
            {
                if (normalTarget.anotherTargetList.Count > 0)
                {
                    if (!one.GetComponent<Enemyhealth>().CheckBtwRapObj())
                    {
                        realTargetList.Add(one);
                    }
                }
            }

            if (realTargetList.Count > 0)
            {
                StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in realTargetList)
            {
                ObjectBase enemy = one.GetComponent<ObjectBase>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.GetComponent<HealthBase>().NormalDamaged(damageNode));
                }
            }
        }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

        else
        {
            if (targetList.Count > 0)
            {
                StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in targetList)
            {
                ObjectBase enemy = one.GetComponent<ObjectBase>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.GetComponent<HealthBase>().NormalDamaged(damageNode));
                }
            }
        }

        if(enemyBulletList.Count > 0)
        {
            foreach (Collider one in enemyBulletList)
            {
                WraithEffect.instance.BulletHit(one.gameObject);
                one.GetComponent<WraithBullet>().onFire = false;
            }
        }
    }

    public void skillAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(skillTarget.targetList);
        List<Collider> realTargetList = new List<Collider>();
        DamageNode damageNode =
            new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.8f, playerEntity.pushBack + 20, 4);

        if (skillTarget.anotherTargetList.Count > 0)
        {
            foreach (Collider one in targetList)
            {
                if (skillTarget.anotherTargetList.Count > 0)
                {
                    if (!one.GetComponent<Enemyhealth>().CheckBtwRapObj())
                    {
                        realTargetList.Add(one);
                    }
                }
            }

            if (realTargetList.Count > 0)
            {
                StartCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in realTargetList)
            {
                EnemyBase enemy = one.GetComponent<EnemyBase>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.GetComponent<HealthBase>().SkillDamaged(damageNode));
                }
            }
        }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

        else
        {
            if (targetList.Count > 0)
            {

                StartCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in targetList)
            {
                ObjectBase enemy = one.GetComponent<ObjectBase>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.GetComponent<HealthBase>().SkillDamaged(damageNode));
                }
            }
        }
    }

    public void ChargeAttack()
    {
        List<Collider> targetList = new List<Collider>(chargeAttackTarget.targetList);
        List<Collider> enemyBulletList = new List<Collider>(chargeAttackTarget.enemyBulletList);
        DamageNode damageNode =
            new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.2f, playerEntity.pushBack + 5, 4);

        foreach (Collider one in targetList)
        {
            ObjectBase enemy = one.GetComponent<ObjectBase>();
            if (enemy != null && !enemy.isDead)
            {
                StartCoroutine(enemy.GetComponent<HealthBase>().NormalDamaged(damageNode));
            }
        }

        if (enemyBulletList.Count > 0)
        {
            foreach (Collider one in enemyBulletList)
            {
                WraithEffect.instance.BulletHit(one.gameObject);
                one.GetComponent<WraithBullet>().onFire = false;
            }
        }
    }
    
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

    public virtual void NormalAttackEvent(int stateNum)
    {
        if (playerEntity.isAttack)  // 중간에 ex무브나 다른 상태로 빠지면 발동안되게끔..
        {
            EffectManager.instance.PlayEffect(gameObject, stateNum, EffectManager.instance.playPlayerAttackEffect);
            NormalAttack(stateNum);
        }
    }
    
    public void ChargeAttackEvent()
    {
        
    }

    public override void NormalAttack()
    {
        
    }

}
