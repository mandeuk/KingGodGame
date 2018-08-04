using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMove : AttackBase {
    PlayerBase playerEntity;
    public GameObject[] rendObjs = new GameObject[5];
    public GameObject[] afterImageRendObjs = new GameObject[5];
    public GameObject afterImageR;

    public float moveSpeed;
    Animator avatar;
    EffectManager effect;
    GameObject skillTarget;
    
    Material[,] afterImageMats = new Material[5, 5];

    protected override void Init()
    {
        base.Init();
        playerEntity = GetComponent<ObjectBase>() as PlayerBase;
        avatar = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        moveSpeed = playerEntity.moveSpeed;
        effect = EffectManager.instance;
        skillTarget = GetComponentInChildren<SkillTarget>().gameObject;
    }

    private void Awake()
    {
        Init();

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(false);
            for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                afterImageMats[i, j] = Instantiate(afterImageRendObjs[i].GetComponent<Renderer>().materials[j]) as Material;
            }
        }
    }

    void Update () {
        if (!playerEntity.isDodge && !playerEntity.isChargeAttack && !playerEntity.isExMoveCooltime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                avatar.SetTrigger("EXMoveOn");
                avatar.SetBool("Combo", false);
                StartCoroutine(EXMovePlay());

            }
        }
    }

    public IEnumerator EXMovePlay()
    {
        playerEntity.isExMoveCooltime = true;
        damageNode = new DamageNode(playerEntity.attackPower, playerEntity.gameObject, 0.2f, playerEntity.pushBack, 4);
        avatar.speed = 0;         // 잔상의 애니메이션을 멈춤

        playerEntity.isExmove = true;    // ex무브가 시작.
        playerEntity.isAttack = false;
        playerEntity.isInvincibility = true;
        playerEntity.ExMoveAttack();
        PlayerBase.instance.GetComponent<Rigidbody>().rotation = skillTarget.transform.rotation;
        PlayerColorChange.instance.PlayerDisappear();   // 라파엘의 보이는 매터리얼들을 다 끔. 그리고 색을 어둡게 바꿈.


        afterImageR.transform.position = transform.position;    // 잔상의 포지션을 라파엘의 위치로 옮김
        rigid.Sleep();          // 잔상의 물리효과를 슬립시킴.        

        playerEntity.isAttack = false;

        for (int i = 0; i < afterImageRendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(true);
            for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                afterImageRendObjs[i].GetComponent<Renderer>().materials[j].color = afterImageMats[i, j].color;
            }
        }   // 잔상의 보이는 매터리얼을 다 킴. 잔상의 색깔을 빈 오브젝트인 afterImageMets 에 다 저장함.
        
        afterImageR.GetComponent<Animator>().speed = 0;           // 이때 잔상의 애니메이션은 가만히 있어야함.
        playerEntity.moveSpeed = 0;
        EffectManager.PlayEffect(gameObject, EffectManager.playEXMoveVanishEffect);
        EffectManager.PlayEffect(gameObject, EffectManager.playEXMoveSlashEffect);

        yield return new WaitForSeconds(0.13f);
        playerEntity.moveSpeed = calcDistObj() * 6;              // ex무브동안의 스피드 이속도로 고속이동함.
        //print(calcDistObj() * 6);       // 검사용 코드. 

        yield return new WaitForSeconds(.05f);
        EffectManager.PlayEffect(gameObject, EffectManager.playExMoveRingEffectBack);

        yield return new WaitForSeconds(.02f);
        EffectManager.PlayEffect(gameObject, EffectManager.playExMoveRingEffectFront);

        yield return new WaitForSeconds(.02f);      // ex무브의 시간인 0.2초
        
        // 캐릭터가 보는방향을 ex무브가 보았던 방향으로 바꿈
        transform.rotation = Quaternion.LookRotation(transform.GetComponentInChildren<SkillTarget>().movePos.normalized);
        transform.GetComponentInChildren<SkillTarget>().transform.rotation
            = Quaternion.LookRotation(transform.forward);
        EffectManager.PlayEffect(gameObject, EffectManager.playEXmoveVanishFlowerEffect);
        //effect.PlayEffect(gameObject, effect.playplayerSwordBlinkEffect);
        for (int i = 0; i < afterImageRendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(false);
        }   // 잔상은 이제 다 꺼야함. -> 필요없을듯 어짜피 알아서
        

        PlayerColorChange.instance.PlayerAppear();        
        PlayerColorChange.instance.PlayerColorChangeBlack();
        rigid.Sleep();

        playerEntity.moveSpeed = moveSpeed;
        avatar.speed = 1;
        afterImageR.GetComponent<Animator>().speed = 1;
        playerEntity.isInvincibility = false;
        playerEntity.isExmove = false;

        yield return new WaitForSeconds(playerEntity.exmoveCoolTime-2.2f);      // ex무브의 시간인 0.2초
        playerEntity.isExMoveCooltime = false;
        yield break;
    }

    float calcDistObj()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.up / 2, transform.forward, out hit, 7, 1 << 10))
        {
            return hit.distance;
        }

        else if (Physics.Raycast(transform.position + transform.up / 2, transform.forward, out hit, 7, 1 << 13))
        {
            return hit.distance;
        }
        else
        {
            return 7;
        }
    }



    public override void NormalAttack()
    {

    }
}
