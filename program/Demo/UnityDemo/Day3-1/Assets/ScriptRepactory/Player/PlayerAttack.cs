using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBase
{
    PlayerBase playerEntity;
    public GameObject normalAttacCam;
    public GameObject exMoveCam;
    protected Animator avatar;
    //public bool b_attacking;

    public NormalTarget normalTarget;
    public SkillTarget skillTarget;
    public ChargeAttackTarget chargeAttackTarget;

    void Awake()
    {
        avatar = GetComponent<Animator>();
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public void NormalAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);
        List<Collider> enemyBulletList = new List<Collider>(normalTarget.enemyBulletList);
        List<Collider> realTargetList = new List<Collider>();

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
        DamageNode damageNode = new DamageNode(PlayerStatus.instance.attackPower, PlayerStatus.instance.gameObject, 0.2f, 5, 1);

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

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            OnAttacking();
        }
    }

    public void OnAttacking()
    {
        avatar.SetBool("Combo", true);
    }

    public void StopAttacking()
    {
        avatar.SetBool("Combo", false);
    }

    public void NormalAttackEvent(int stateNum)
    {
        if (transform.CompareTag("Player") || playerEntity.isAttack)
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

        //throw new System.NotImplementedException();
    }

}
