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
    static PlayerStatus playerStatus;
    public float energy, attackSpeed, attackRange, devilGage, etere, stance, exmoveCoolTime;
    public bool isExmove, isChargeAttack, isDodge;
    public bool isExMoveCooltime;

    Animator anim;
    Rigidbody rigid;
    
    void Awake()
    {
        Init();
    }

    private void Start()
    {
        GameManager.PlayerItemLoad();
    }

    protected override void Init()
    {
        instance = this;

        base.Init();
        playerStatus = GameManager.PlayerStatusLoad();

        maxHP = playerStatus.maxHP;      // 맥스 hp
        curHP = playerStatus.curHP;      // 현재 hp
        attackPower = playerStatus.attackPower;     // 공격력
        attackSpeed = playerStatus.attackSpeed;      // 공격속도
        attackRange = playerStatus.attackRange;      // 공격 범위
        moveSpeed = playerStatus.moveSpeed;      // 이동 속도
        pushBack  = playerStatus.pushBack;      // 넉백을 주는 힘
        energy = playerStatus.energy;      // 에너지
        etere = playerStatus.etere;      // 에테르
        devilGage = playerStatus.devilGage;     // 폭주 게이지 100이되면 죽음.
        stance = playerStatus.stance;      // 성향. 구원,타락 수치로 성향이 높아지고 낮아진다.
        exmoveCoolTime = 8;

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
        GetComponent<PlayerAttack>().skillAttack(4);
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
    public IEnumerator PlayerActSceneStart()
    {
        PlayerColorChange.instance.PlayerDisappear();

        yield return new WaitForSeconds(2f);
        PlayerColorChange.instance.PlayerAppear();
        EffectManager.PlayEffect(gameObject, EffectManager.playEXmoveVanishFlowerEffect);
        GetComponent<Animator>().SetTrigger("roomMove");

        yield break;
    }

    // 함수 기능 :  스텟을 관리하는 델리게이트 선언
    public delegate void SetStat(float amount, bool up);

    // 함수 기능 :  스탯을 올리고 내리고를 여기서 처리함.
    //              예를들어 hp가 올라가고 내려가고를 cur/ MAX 구분할것 없이
    //              이곳 함수에서 올라가면 올라간대로 내려가면 내려간대로 처리함.
    public void SetStatus(float amount, bool up, SetStat status)
    {
        status(amount, up);
        GameManager.PlayerStatusSave(playerStatus);
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
        playerStatus.maxHP = maxHP;
        playerStatus.curHP = curHP;
        PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
    }
    
    public void CurHP(float amount, bool up)
    {
        if (up)
        {
            if ((int)curHP > (int)maxHP)
            {
                curHP += amount;
            }
        }
        else
        {
            curHP -= amount;
        }
        playerStatus.curHP = curHP;
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
        playerStatus.attackPower = attackPower;
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
        playerStatus.attackSpeed = attackSpeed;
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
        playerStatus.attackRange = attackRange;
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
        playerStatus.moveSpeed = moveSpeed;
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
        playerStatus.pushBack = pushBack;
    }

    public void Energy(float amount, bool up)
    {
        if (up)
        {
            energy += amount;
            PlayerColorChange.instance.PlayerColorChangeYellow();
        }
        else
        {
            energy -= amount;
        }
        playerStatus.energy = energy;
        PlaySceneUIManager.instance.ChangeEnergyAmountText();
    }

    public void Etere(float amount, bool up)
    {
        if (up)
        {
            etere += amount;
            PlayerColorChange.instance.PlayerColorChangeBlue();
        }
        else
        {
            etere -= amount;
        }

        playerStatus.etere = etere;
        PlaySceneUIManager.instance.ChangeEterAmountText();
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
        playerStatus.devilGage = devilGage;
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
        playerStatus.stance = stance;
        // 스텐스에 따라서 나눠줘야함. bool로 나누든..
    }

    public void PlayerStiff()
    {
        StartCoroutine(Stiff());
    }

    IEnumerator Stiff()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.07f);
        Time.timeScale = 1.3f;
        yield return new WaitForSecondsRealtime(0.03f);
        Time.timeScale = 1.0f;
        yield break;
    }

    IEnumerator Devillization()
    {

        yield break;
    }
}
