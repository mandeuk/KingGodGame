using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : ObjectBase {
    public bool onFire;
    public GameObject Attacker;

    // Use this for initialization
    private void OnEnable()
    {
        entityDamagedCall += new DamagedEventHandler(judgeDamageNode);
        StartCoroutine(VanishingEffect());
        Init();
    }

    protected override void Init()
    {
        base.Init();
        onFire = true;
        maxHP = 1;
        curHP = 1;
    }

    // 데미지를 판단해주는 함수.
    void judgeDamageNode(DamageNode damageNode)
    {
        if (--curHP < 1)
        {
            BulletHit();
            return;
        }
        // 에너지가 있는 총알일경우 푸시백이 되어야함.. 이런건 아마 헬스가 생길듯
        // 추후 추가바람.
    }

    // 일단 일괄적으로  damaged 를 부르기때문에 얘도 damaged 이벤트 호출을 같이 받으려면
    // 안쓰이는 damageNode를 받아와야함..
    public virtual void BulletHit()
    {
        onFire = false;

        Attacker.GetComponent<RangeAttack>().BulletHit(gameObject);
    }

    // 함수 기능 :  5초뒤에 사라지게 함.
    public virtual IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(4.0f);
        BulletHit();

        yield break;
    }

    // 몬스터가 죽어도 총알은 남아있는편이 더 재밌을거같음.
    private void OnDisable()
    {
        entityDamagedCall -= new DamagedEventHandler(judgeDamageNode);
    }
}
