using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour {
    protected ObjectBase entity;
    protected Rigidbody rigid;

    IEnumerator NormalDamagedCo;
    IEnumerator SkillDamagedCo;

    protected virtual void Init()
    {
        entity = GetComponent<ObjectBase>();
        rigid = GetComponent<Rigidbody>();
    }

    // 함수 기능 : 이벤트 등록. 맞고, 죽고.
    public virtual void OnEnable()
    {
        entity.entityDamagedCall += new DamagedEventHandler(Damaged);
        entity.entityDeadCall += new DeadEventHandler(Death);
    }

    // 함수 기능 : 외부에서 StartCoroutine을 부르지 않고 내부에서 부르기 위한 함수. 
    // 위의 이뉴머레이터들로 가져와서 덧입혀서 스탑코루틴 스타트코루틴 해줘야함. 나중에수정합시다.
    public virtual void Damaged(DamageNode damageNode)
    {
        if (!entity.isInvincibility && !entity.isDead)
        {
            StartCoroutine(NormalDamaged(damageNode));
        }
    }

    // 함수 기능 : 캐릭터가 밀리고, 딜레이 시간만큼 데미지 상태를 체크하고, 딜레이 시간 뒤에 멈추게함
    //              일단 이뉴머레이터로 만듬. 이뉴머 레이터로 만들어서 자식객체가 상속받기 힘듬. 무언가 추가하기가 어려움
    //              그래서 이뉴머레이터를 포기할까 싶긴한데 일단 쓰고 TakeDamage를 override 로 바꾸는 형식으로 구현.
    //              TakeDamage로 상속한 자식객체마다의 다형성을 구현. <- 결론.

    //              하다가 보니까 저게 더 거지같음 그냥 노말데미지 자체를 계속 오버라이드해서 구현하는게 나을거같다 망할
    public virtual IEnumerator NormalDamaged(DamageNode damageNode)
    {
        entity.isDamaged = true;
        TakeDamage(damageNode);

        yield return new WaitForSeconds(damageNode.delay);
        if (!entity.isDead)
        {
            AfterDamage(damageNode);
        }
        entity.isDamaged = false;

        yield break;
    }

    // 함수 기능 :  딜레이시간 전의 맞는 처리. 이걸 오버로딩해서 사용하는게 좋을듯..
    public virtual void TakeDamage(DamageNode damageNode)
    {
        if (!entity.isSuperArmor)
        {
            Vector3 diff = damageNode.attacker.transform.position - transform.position;
            rigid.AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 400f * damageNode.pushBack);
        }

        if (entity.curHP < 1.0f && !entity.isDead)
        {
            entity.Dead();
        }
    }

    // 이거 에프터데미지에 왜 데미지노드가 있어야하는거엿더라..?
    public virtual void AfterDamage(DamageNode damageNode)
    {
        rigid.Sleep();
        entity.isDamaged = false;
    }

    public virtual void Death()
    {
        entity.isDead = true;
    }

    private void OnDisable()
    {
        entity.entityDamagedCall -= new DamagedEventHandler(Damaged);
        entity.entityDeadCall -= new DeadEventHandler(Death);
    }
}
