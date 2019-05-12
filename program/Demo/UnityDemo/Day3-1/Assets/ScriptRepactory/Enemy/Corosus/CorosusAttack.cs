using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//1점프 -> 플레이어에게 빠르게 접근 후 점프.그냥 제자리 동그란 원 커지면서 공격(지금과 동일 동그란 토러스모양만 더 진하게)
//2점프 -> 플레이어에게 접근 하지 않고 점프.플레이어 위치로 낙하, 2~4번 랜덤, 미리 떨어질 범위를 알려줌(블소처럼)
//3점프 -> 플레이어에게 접근 하지 않고 점프.제자리 낙하 후 Tar들이 떨어짐.그후 대쉬고정.

// 정확하게 스크립트나 코루틴으로 처리할거면 하고
// 애니메이터 기반으로 할거면 했어야 하는데
// 저건 애니메이터비헤이비어 기반/ 저건 코루틴기반 이런식으로해서 에러가 남.
// 각각 장단점이 있기에 바꾸기 어려워보임.

public class CorosusAttack : MeleeAttack
{
    CorosusBase corosusEntity;
    public GameObject dashAttackAtrea;
    public GameObject DashEffect;
    protected NavMeshAgent nav;
    Collider col;   

    int jumpCnt = 0;
    // hardPat은 타르 떨구고 
    bool hardPat = false, afterJump1 = false;

    IEnumerator jumpMovingCo;
    
    void NormalAttackPattern()
    {
        print("NormalPattern");

        hardPat = true;
        print("HardPattern");

        //switch (Random.Range(1, 4))
        //{
        //    case 1:
        //        anim.SetBool("Attack", true);
        //        break;
        //    case 2:
        //        anim.SetBool("JumpAttack2", true);
        //        break;
        //    case 3:
        //        hardPat = true;
        //        print("HardPattern");
        //        break;

        //    default:
        //        anim.SetBool("JumpAttack2", true);
        //        break;
        //}
    }

    // 점프1을 해서 Tar들을 떨구고 난뒤 Dash나 다중점프.
    void SpecialAttackPattern()
    {
        if (afterJump1)
        {
            print("AfterJump");

            afterJump1 = false;
            hardPat = false;
            NormalAttackPattern();
        }
        else
        {
            print("JumpAttackTar");
            anim.SetTrigger("JumpAttack");
            afterJump1 = true;
        }
    }

    // Use this for initialization
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        corosusEntity = entity as CorosusBase;
        nav = GetComponent<NavMeshAgent>();
        jumpMovingCo = onJumpMoving();
        col = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        AttackUpdate();
    }

    protected override void AttackUpdate()
    {
        if (corosusEntity.isAgro && !corosusEntity.isAttack && !corosusEntity.isDealTime)
        {
            Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < corosusEntity.attackDistance
                && !corosusEntity.isTurn)
            {
                StartAttack();
            }
        }
    }

    public override void StartAttack()
    {
        if (hardPat)
            SpecialAttackPattern();
        else
            NormalAttackPattern();
    }

    //-----------------------------------------------//

    // invoke로 불리고있음.
    // 딜타임을 주기 위하여 시간이 필요하기때문.
    void AnimToIdle()
    {
        anim.SetTrigger("StopDealTime");
        Invoke("StopDealTime", Random.Range(2.5f, 3.5f));
    }

    // invoke로 불리고있음.
    // 딜타임이 끝나도 회복하여 찾기전까지는 시간이 좀 있어야 자연스럽기 때문에
    // 따로 떼서 invoke로 호출함.
    void StopDealTime()
    {
        corosusEntity.isDealTime = false;
    }

    // 이것도 아래와 마찬가지로 따로 띄어놓는데
    // 아무래도 쓸모없을거같긴합니다...
    public void JumpChaseStart()
    {
        StartCoroutine(jumpMovingCo);
    }

    // 점프해서 유저를 쫒는걸 점프 첨부터 끝까지하면 좀 이상해서
    // 점프해서 유저찾는거는 함수를 따로 띄어놓습니다.
    public void JumpChaseEnd()
    {
        // 코루틴을 등록하고 쓰게되면 중단된 지점부터 다시 시작하게됨.
        nav.speed = 0;
        StopCoroutine(jumpMovingCo);
        jumpMovingCo = onJumpMoving();
    }

    public void Jump1Start()
    {
        corosusEntity.InvincibilityOn();
    }

    public void Jump1Landing()
    {
        corosusEntity.isMovingJump = false;
        corosusEntity.isTurn = false;
        corosusEntity.InvincibilityOff();
        EventManager.CorosusJump1Attack();

        EventManager.CameraMoveEvent((int)CameraMoveType.Normal);
        col.enabled = true;

        Invoke("AnimToIdle", Random.Range(1.5f, 2.5f));        
    }

    public void Jump2Start()
    {
        JumpChaseStart();
        corosusEntity.InvincibilityOn();
    }

    public void Jump2Landing()
    {
        corosusEntity.isMovingJump = false;
        corosusEntity.isTurn = false;
        corosusEntity.InvincibilityOff();
        
        EventManager.CameraMoveEvent((int)CameraMoveType.Normal);
        EffectManager.playCorosusJumpAttackEffect(gameObject);
        col.enabled = true;

        if (Random.Range(1, 7) < 3 || jumpCnt > 3)
        {
            jumpCnt = 0;
            corosusEntity.isAttack = false;
            corosusEntity.isDealTime = true;

            anim.SetBool("JumpAttack2", false);
            Invoke("AnimToIdle", Random.Range(2.0f, 3.5f));
        }

        base.NormalAttack();
    }

    // 점프어택 땅에 찍을때 발동될거임
    public override void NormalAttack()
    {
        EventManager.CameraMoveEvent((int)CameraMoveType.EX);
        //EXMoveCam.instance.playEXCameraEvent(1);
        EffectManager.playCorosusJumpAttackEffect(gameObject);
        base.NormalAttack();
        // 맞는거 어택트리거로 해놔야됨.
    }
    
    IEnumerator onJumpMoving()
    {
        corosusEntity.isMovingJump = true;
        col.enabled = false;
        jumpCnt++;

        float timer = new float();
        while (timer < 4.0f)
        {
            nav.destination = player.transform.position;
            nav.speed = corosusEntity.moveSpeed;
            timer += Time.fixedDeltaTime;
            Turn();
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    public void DashStart()
    {
        StartCoroutine(Dashing());
    }

    public void Dash()
    {
        dashAttackAtrea.GetComponent<AttackTrigger>().damageNode = damageNode;
        dashAttackAtrea.GetComponent<CorosusAttackTrigger>().attacker = this.gameObject;
        dashAttackAtrea.SetActive(true);
    }

    public void Break()
    {
        DashEffect.SetActive(false);
        EffectManager.playCorosusBreakEffect(gameObject);
        SoundManager.playCorosusBreak();
    }

    IEnumerator Dashing()
    {
        corosusEntity.isAttack = true;
        yield return new WaitForSecondsRealtime(1.6f);
        DashEffect.SetActive(true);

        yield return new WaitForSecondsRealtime(0.3f);
        float timer = new float();

        while (timer < 7.0f && !corosusEntity.isBreak)
        {
            timer += Time.fixedDeltaTime;
            rigid.MovePosition(transform.position + transform.forward * Time.deltaTime * corosusEntity.dashSpeed);

            yield return new WaitForEndOfFrame();
        }
        dashAttackAtrea.SetActive(false);
        anim.SetTrigger("Break");
        corosusEntity.isDealTime = true;

        yield return new WaitForSecondsRealtime(Random.Range(3.5f, 5f));
        anim.SetTrigger("StopDealTime");
        corosusEntity.isBreak = false;

        yield return new WaitForSecondsRealtime(1.5f);
        corosusEntity.isDealTime = false;
        yield break;
    }

    public void AttackReadyEvent()
    {
        SoundManager.playCorosusAttackReady();
    }
}
