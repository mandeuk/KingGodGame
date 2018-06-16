using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    MAXHP,
    CURHP,
    ATTACKPOWER,
    ATTACKSPEED,
    ATTACKRANGE,
    MOVESPEED,
    PUSHBACK,
    ENERGY,
    ETERE,
    DEVILGAGE,
    STANCE
}

public class PlayerBase : ObjectBase {
    public static PlayerBase instance = null;
    public float energy, attackSpeed, attackRange, devilGage, etere, stance;
    public bool isExmove, isChargeAttack, isDodge;

    Animator anim;
    Rigidbody rigid;
    
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        if (instance)//인스턴스가 생성되어있는가?
        {
            DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }

        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }

        base.Init();

        maxHP =         5;      // 맥스 hp
        curHP =         5;      // 현재 hp
        attackPower =   30;     // 공격력
        attackSpeed =   1;      // 공격속도
        attackRange =   1;      // 공격 범위
        moveSpeed =     8;      // 이동 속도
        pushBack  =     5;      // 넉백을 주는 힘
        energy =        3;      // 에너지
        etere =         0;      // 에테르
        devilGage =     30;     // 폭주 게이지 100이되면 죽음.
        stance =        0;      // 성향. 구원,타락 수치로 성향이 높아지고 낮아진다.

        isInvincibility = false;
        isExmove = false;
        isChargeAttack = false;
        isDodge = false;

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        anim.SetFloat("AttackSpeed", attackSpeed);
        anim.SetFloat("MoveSpeed", moveSpeed / 8);
    }

    public override void Attack()
    {
        
    }
    public void ExMoveAttack()
    {
        
    }
    public override void Damaged(DamageNode damageNode)
    {
        GetComponent<PlayerHealth>().Damaged(damageNode);
    }
    public override void Dead()
    {

    }
    public void PlayerEnable()
    {
        transform.GetComponent<PlayerMovement>().enabled = true;
        transform.GetComponent<PlayerHealth>().enabled = true;
        transform.GetComponent<PlayerAttack>().enabled = true;
        transform.GetComponent<EXMove>().enabled = true;
        transform.GetComponent<ExplosionAttack>().enabled = true;
        //transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Collider>().isTrigger = false;
    }
    public void PlayerDisable()
    {
        transform.GetComponent<PlayerMovement>().enabled = false;
        transform.GetComponent<PlayerHealth>().enabled = false;
        transform.GetComponent<PlayerAttack>().enabled = false;
        transform.GetComponent<EXMove>().enabled = false;
        transform.GetComponent<ExplosionAttack>().enabled = false;
        //transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Collider>().isTrigger = true;
    }

    // 함수 기능 :  스텟을 관리하는 델리게이트 선언
    public delegate void SetStat(float amount, bool up);

    // 함수 기능 :  스탯을 올리고 내리고를 여기서 처리함.
    //              예를들어 hp가 올라가고 내려가고를 cur/ MAX 구분할것 없이
    //              이곳 함수에서 올라가면 올라간대로 내려가면 내려간대로 처리함.
    public void SetStatus(float amount, bool up, SetStat status)
    {
        status(amount, up);
    }

    // 함수 기능 :  스탯의 정보를 가져감. 한꺼번에 관리하고 일관성 있게 하려면 필요하긴 할듯
    //              일단 불편하기도하고 안쓸거임. 일관성있게 하려면 델리게이트까지 동원하는게 좋을듯.
    public float GetStatus(Status stat)
    {
        switch (stat)
        {
            case Status.ATTACKPOWER:
                return attackPower;
            case Status.ATTACKRANGE:
                return attackRange;
            case Status.ATTACKSPEED:
                return attackSpeed;
            case Status.CURHP:
                return curHP;
            case Status.DEVILGAGE:
                return devilGage;
            case Status.ENERGY:
                return energy;
            case Status.ETERE:
                return etere;
            case Status.MAXHP:
                return maxHP;
            case Status.MOVESPEED:
                return moveSpeed;
            case Status.PUSHBACK:
                return pushBack;
            case Status.STANCE:
                return stance;
            default:
                return 0;
        }
    }

    public void MaxHP(float amount, bool up)
    {
        if (up)
        {
            if ((int)maxHP < 14)    // 최대체력(14)보다 클경우
            {
                maxHP += amount;
                curHP += amount;
            }
        }
        else
        {
            if ((int)maxHP > 1)     // 최소체력(1)보다 작을경우
            {
                maxHP -= amount;
                curHP -= amount;
            }
        }
        PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
    }
    
    public void CurHP(float amount, bool up)
    {
        if (up)
        {
            if((int)curHP > (int)maxHP)
            {
                curHP += amount;
            }
        }
        else
        {
            if((int)curHP < 1)
            {
                Dead();
            }
        }
        PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
    }

    public void AttackPower(float amount, bool up)
    {
        if (up)
        {
            attackPower += amount;
        }
        else
        {
            attackPower -= amount;
        }
    }

    public void AttackSpeed(float amount, bool up)
    {
        if (up)
        {
            attackSpeed += amount;
        }
        else
        {
            attackSpeed -= amount;            
        }
        anim.SetFloat("AttackSpeed", attackSpeed);
    }

    public void AttackRange(float amount, bool up)
    {
        if (up)
        {

        }
        else
        {

        }
    }

    public void MoveSpeed(float amount, bool up)
    {
        if (up)
        {
            moveSpeed += amount;
        }
        else
        {
            moveSpeed -= amount;
        }
        anim.SetFloat("MoveSpeed", moveSpeed / 8);
    }

    public void PushBack(float amount, bool up)
    {
        if (up)
        {
            pushBack += amount;
        }
        else
        {
            pushBack -= amount;
        }
    }

    public void Energy(float amount, bool up)
    {
        if (up)
        {
            energy += amount;
        }
        else
        {
            energy -= amount;
        }
    }

    public void Etere(float amount, bool up)
    {
        if (up)
        {
            etere += amount;
        }
        else
        {
            etere -= amount;
        }
    }

    public void DevilGage(float amount, bool up)
    {
        if (up)
        {
            devilGage += amount;
        }
        else
        {
            devilGage -= amount;
        }


    }

    public void Stance(float amount, bool up)
    {
        if (up)
        {
            stance += amount;
        }
        else
        {
            stance -= amount;
        }

        // 스텐스에 따라서 나눠줘야함. bool로 나누든..
    }
}
