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

    IEnumerator jumpMovingCo;
    IEnumerator dashAttackCo;

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
        col = GetComponent<Collider>();
    }

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {
        AttackUpdate();
    }


    protected override void AttackUpdate()
    {
        if (corosusEntity.isAgro && !corosusEntity.isAttack)
        {
            if (!corosusEntity.isDealTime)
                Turn();

            if (Vector3.Distance(player.transform.position, transform.position) < corosusEntity.attackDistance
                && !corosusEntity.isTurn && !corosusEntity.isDealTime)
            {
                StartAttack();
            }
        }
    }

    public override void StartAttack()
    {
        //if (Random.Range(1, 5) < 3)
        //{
        //    anim.SetBool("Attack", true);
        //}
        //else
        //{
        //anim.SetBool("JumpAttack", true);
        anim.SetBool("JumpAttack2",true);
        //}
    }

    //-----------------------------------------------//

    // 외부에서 호출될, 딜타임을 끝내고 아이들상태가 되게 만들어 주는 함수.
    // 인보크를 이용해서 딜 시간을 조절함.
    public void StopDealing()
    {
        print("stopDealing");

        Invoke("StopDealTime",Random.Range(3.0f,5.0f));
    }

    void StopDealTime()
    {
        print("Move");

        anim.SetTrigger("StopDealTime");
    }

    public void Jump1Start()
    {
        
    }

    public void Jump1Landing()
    {

    }

    public void Jump2Start()
    {
        print("jumpStart");
        StartCoroutine(jumpMovingCo);
        corosusEntity.InvincibilityOn();
    }

    public void Jump2Landing()
    {
        print("jumpEnd");

        StopCoroutine(jumpMovingCo);
        corosusEntity.isMovingJump = false;
        corosusEntity.InvincibilityOff();
        nav.speed = 0;
        EventManager.CameraMoveEvent((int)CameraMoveType.Normal);
        EffectManager.playCorosusJumpAttackEffect(gameObject);
        col.isTrigger = true;
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
        col.isTrigger = false;

        float timer = new float();

        while (timer < 7.0f)
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
