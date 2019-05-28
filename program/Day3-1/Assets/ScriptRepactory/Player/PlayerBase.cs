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

    // 이거 왜 static이지? 나중에한번 지워봐야함.
    static PlayerStatus playerStatus;

    public float energy, attackSpeed, attackRange, devilGage, etere, stance, exmoveCoolTime;
    public bool isExmove, isChargeAttack, isDodge, isDevil, isFly;
    public bool isExMoveCooltime;
    public Vector2 curRoomPoint;

    Animator anim;
    Rigidbody rigid;
    IEnumerator DevilCo;

    void Awake()
    {
        Init();
    }

    private void Start()
    {
        GameManager.PlayerItemLoad();
        StartCoroutine(DevilCo);
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
        pushBack = playerStatus.pushBack;      // 넉백을 주는 힘
        energy = playerStatus.energy;      // 에너지
        etere = playerStatus.etere;      // 에테르
        devilGage = playerStatus.devilGage;     // 폭주 게이지 100이되면 죽음.
        stance = playerStatus.stance;      // 성향. 구원,타락 수치로 성향이 높아지고 낮아진다.
        exmoveCoolTime = 8;

        isInvincibility = false;
        isExmove = false;
        isChargeAttack = false;
        isDodge = false;
        isDevil = false;
        isFly = true;

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        anim.SetFloat("AttackSpeed", attackSpeed);
        anim.SetFloat("MoveSpeed", moveSpeed / 8);

        DevilCo = DevillizationCo();
    }

    private void Update()
    {

        // 본래상태로 되돌리는 cheat
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Cheat();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            InstanceDead();
        }

        // 좀 세지는 cheat
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            RealCheat();
        }

        // 7번 누르면 데빌게이지가 79로 고정.
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GameManager.instance.MoveStage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetStatus(50, false, DevilGage);
            ReturnDevil();
        }
    }

    public override void Attack()
    {

    }

    public void ExMoveAttack()
    {
        GetComponent<PlayerAttack>().SkillAttack();
    }

    public override void Damaged(DamageNode damageNode)
    {
        GetComponent<PlayerHealth>().Damaged(damageNode);
    }

    public void Devillization()
    {
        if (!isDevil)
        {
            isDevil = true;
            CameraEventManager.instance.DevillizationCameraEvent();
            GetComponent<PlayerEffect>().playOuraEffect();
            EventManager.CameraMoveEvent((int)CameraMoveType.EX);
            //EXMoveCam.instance.playEXCameraEvent(1);
            SetStatus(20, true, AttackPower);
            SetStatus(0.3f, true, AttackSpeed);
            SetStatus(1f, true, MoveSpeed);
        }
    }

    public void ReturnDevil()
    {
        isDevil = false;
        GetComponent<PlayerEffect>().stopOuraEffect();
        CameraEventManager.instance.ReturnDevilCameraEvent();

    }

    public void EndDevil()
    {
        ReturnDevil();
        StopCoroutine(DevilCo);
        devilGage = 30;
        playerStatus.devilGage = devilGage;
        EncroachmentManager.instance.UpdateDevilGauge();
        GameManager.PlayerStatusSave(playerStatus);
    }

    void InstanceDead()
    {
        Damaged(new DamageNode(1, new GameObject(), 0.1f, 1, 1));
    }

    // 데빌게이지는 데모버전 시연을위해 뺌.
    public void RealCheat()
    {
        maxHP = 10;      // 맥스 hp
        curHP = 10;      // 현재 hp
        attackPower = 60;     // 공격력
        attackSpeed = 1.5f;      // 공격속도
        moveSpeed = 9;      // 이동 속도

        playerStatus.moveSpeed = moveSpeed;
        anim.SetFloat("MoveSpeed", moveSpeed / 8);
        playerStatus.maxHP = maxHP;
        playerStatus.curHP = curHP;
        PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
        playerStatus.attackPower = attackPower;
        playerStatus.attackSpeed = attackSpeed;
        anim.SetFloat("AttackSpeed", attackSpeed);

        GameManager.PlayerStatusSave(playerStatus);
    }

    void Cheat()
    {
        maxHP = 8;      // 맥스 hp
        curHP = 8;      // 현재 hp
        attackPower = 30;     // 공격력
        attackSpeed = 1;      // 공격속도
        moveSpeed = 8;      // 이동 속도
        devilGage = 30;     // 폭주 게이지 100이되면 죽음.

        playerStatus.devilGage = devilGage;
        EncroachmentManager.instance.UpdateDevilGauge();

        if (devilGage > 80)
        {
            Devillization();
        }
        playerStatus.moveSpeed = moveSpeed;
        anim.SetFloat("MoveSpeed", moveSpeed / 8);
        playerStatus.maxHP = maxHP;
        playerStatus.curHP = curHP;
        PlaySceneUIManager.instance.UpdateHPUI(); //체력UI 갱신 함수
        playerStatus.attackPower = attackPower;
        playerStatus.attackSpeed = attackSpeed;
        anim.SetFloat("AttackSpeed", attackSpeed);

        ReturnDevil();

        GameManager.PlayerStatusSave(playerStatus);
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
            if ((int)maxHP < 10)    // 최대체력(14)보다 클경우
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
            if ((int)curHP < (int)maxHP)
            {
                curHP += amount;
            }
        }
        else
        {
            curHP -= amount;
        }
        playerStatus.curHP = curHP;
        if(PlaySceneUIManager.instance != null)
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
            if (attackSpeed < 2)
            {
                attackSpeed += amount;
            }
            else
            {
                attackSpeed = 2;
            }
        }
        else
        {
            if (attackSpeed > 0.6f)
            {
                attackSpeed -= amount;
            }
            else
            {
                attackSpeed = 0.6f;
            }
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
            if (moveSpeed < 14)
            {
                moveSpeed += amount;
            }
            else
            {
                moveSpeed = 14;
            }
        }
        else
        {
            if (moveSpeed > 3)
            {
                moveSpeed -= amount;
            }
            else
            {
                moveSpeed = 3;
            }
        }
        playerStatus.moveSpeed = moveSpeed;
        anim.SetFloat("MoveSpeed", 7 + moveSpeed / 8);
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
        //PlaySceneUIManager.instance.ChangeEnergyAmountText();
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
        //PlaySceneUIManager.instance.ChangeEterAmountText();
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
        //EncroachmentManager.instance.UpdateDevilGauge();

        if (devilGage > 80)
        {
            Devillization();
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
        playerStatus.stance = stance;
        // 스텐스에 따라서 나눠줘야함. bool로 나누든..
    }

    public void PlayerStiff()
    {
        StartCoroutine(Stiff());
    }


    // 이렇게 애니메이션으로 조절하면 이펙트가 안멈춤...
    // 같이 멈추려면 시간을 멈추는게 일단은 답...
    // 나중에 수정합시다 다같이멈추도록
    //IEnumerator Stiff()
    //{
    //    anim.speed = 0.0f;
    //    yield return new WaitForSecondsRealtime(0.7f);
    //    anim.speed = 1.3f;
    //    yield return new WaitForSecondsRealtime(0.3f);
    //    anim.speed = 1.0f;
    //    yield break;
    //}

    IEnumerator Stiff()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(0.07f);
        Time.timeScale = 1.3f;
        yield return new WaitForSecondsRealtime(0.03f);
        Time.timeScale = 1.0f;
        yield break;
    }

    IEnumerator DevillizationCo()
    {
        while (devilGage < 100)
        //while(true)
        {
            if (!isDevil)
            {
                SetStatus(0.045f, true, DevilGage);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                //yield break;
                SetStatus(0.012f, true, DevilGage);
                yield return new WaitForSeconds(0.1f);
            }
        }
        Dead();
        yield break;
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
}
