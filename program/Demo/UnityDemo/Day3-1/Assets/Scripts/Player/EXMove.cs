using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMove : MonoBehaviour {
    public GameObject[] rendObjs = new GameObject[5];
    public GameObject[] afterImageRendObjs = new GameObject[5];
    
    public GameObject afterImageR;

    public float moveSpeed;
    public bool onEXMove;
    public bool onEXMoveColor;
    public bool onAfterImageChange;

    Animator avatar;
    PlayerEffect EXEffect;

    Material[,] raphaelMats = new Material[5, 5];    // 라파엘 매터리얼 보관해두는 변수
    Material[,] afterImageMats = new Material[5, 5];

    // Use this for initialization
    void Awake () {
        avatar = transform.GetComponent<Animator>();
        moveSpeed = transform.GetComponent<PlayerStatus>().getMoveSpeed();
        EXEffect = transform.GetComponent<PlayerEffect>();

        //afterImageMat = Resources.Load("Materials/AfterImageEffectMat") as Material;
        
        onEXMove = false;
        onEXMoveColor = false;
        onAfterImageChange = false;

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                raphaelMats[i,j] = Instantiate(rendObjs[i].GetComponent<Renderer>().materials[j]) as Material;
            }
        }

        // 메터리얼을 인스턴스화 해서 저장한뒤 나중에 색깔을 빼서 쓰려고 만듬.
        // 이제 무조건 배열안에 넣을때는 인스턴스 해서 넣기 알았지? **

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(false);
            for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                afterImageMats[i, j] = Instantiate(afterImageRendObjs[i].GetComponent<Renderer>().materials[j]) as Material;
            }
        }

        // 잔상의 게임오브젝트를 다 꺼줌.
        // 잔상의 매터리얼을 인스턴스화 해서 컬러값을 저장해놓을거임.

        //for (int i = 0; i < rendObjs.Length; ++i)
        //{
        //    for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
        //    {
        //        changeMats = rendObjs[i].GetComponent<Renderer>().materials;
        //        changeMats[j] = afterImageMat;
        //        rendObjs[i].GetComponent<Renderer>().materials = changeMats;
        //    }
        //}

        // 메터리얼이 2개이상있는 메쉬를 바꾸는 방법인 코드.
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            avatar.SetTrigger("EXMoveOn");
            avatar.SetBool("Combo", false);
            //onAfterImageChange = false;
            //onEXMoveColor = false;
            //onEXMove = false;
            StartCoroutine(EXMovePlay());
        }

        //if (onAfterImageChange)
        {
            for (int i = 0; i < afterImageRendObjs.Length; ++i)
            {
                for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    afterImageRendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                            "_Color", Color.Lerp(afterImageRendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_Color"),
                            new Vector4(1, 1, 1, 0), Time.deltaTime * 4f));
                }
            }
        }

        if (onEXMoveColor)  
        {
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                        "_Color", Color.Lerp(rendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_Color"),
                        raphaelMats[i, j].color, Time.deltaTime * 3.5f));
                }
            }
        }
    }

    public IEnumerator EXMovePlay()
    {
        onEXMove = true;    // ex무브가 시작.
        onEXMoveColor = false;
        transform.GetComponent<PlayerAttack>().skillAttack(4);
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].SetActive(false);
                rendObjs[i].GetComponent<Renderer>().materials[j].color = new Vector4(0, 0, 0, 0);
            }
        }   // 라파엘의 보이는 매터리얼들을 다 끔. 그리고 색을 어둡게 바꿈.

        afterImageR.transform.position = transform.position;    // 잔상의 포지션을 라파엘의 위치로 옮김
        afterImageR.GetComponent<Animator>().speed = 0;         // 잔상의 애니메이션을 멈춤
        afterImageR.GetComponent<Rigidbody>().Sleep();          // 잔상의 물리효과를 슬립시킴.        
        transform.GetComponent<PlayerAttack>().b_attacking = false;

        for (int i = 0; i < afterImageRendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(true);
            for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                afterImageRendObjs[i].GetComponent<Renderer>().materials[j].color = afterImageMats[i, j].color;
            }
        }   // 잔상의 보이는 매터리얼을 다 킴. 잔상의 색깔을 빈 오브젝트인 afterImageMets 에 다 저장함.

        onAfterImageChange = true;  // 잔상을 투명하게하는 update조건문. 점점 투명해짐
        avatar.speed = 0;           // 이때 잔상의 애니메이션은 가만히 있어야함.

        transform.GetComponent<PlayerStatus>().moveSpeed = 1;
        EXEffect.playEXMoveVanishEffect();    // ex무브의 이펙트 발동
        EXEffect.playEXMoveSlashEffect();
        //transform.rotation = Quaternion.LookRotation(GetComponentInChildren<SkillTarget>().transform.forward);

        yield return new WaitForSeconds(0.13f);
        transform.GetComponent<PlayerStatus>().moveSpeed = calcDistObj() * 7;              // ex무브동안의 스피드 이속도로 고속이동함.
        
        onEXMoveColor = false;

        yield return new WaitForSeconds(.02f);
        EXEffect.playExMoveRingEffectBack();

        yield return new WaitForSeconds(.02f);
        EXEffect.playExMoveRingEffectFront();
        onEXMoveColor = false;

        yield return new WaitForSeconds(.03f);      // ex무브의 시간인 0.2초
        onEXMove = false;
        transform.rotation = Quaternion.LookRotation(transform.GetComponentInChildren<SkillTarget>().movePos.normalized);
        EXEffect.playEXmoveVanishFlowerEffect();
        EXEffect.playBlinkEffect();
        for (int i = 0; i < afterImageRendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(false);
        }   // 잔상은 이제 다 꺼야함. -> 필요없을듯 어짜피 알아서

        onEXMoveColor = true;   // 라파엘 본체의 컬러를 검정에서 원래색으로 돌려주는 업데이트를 작동

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].SetActive(true);
            }
        }
        transform.GetComponent<PlayerStatus>().moveSpeed = moveSpeed;
        avatar.speed = 1;
        afterImageR.GetComponent<Animator>().speed = 1;

        yield return new WaitForSeconds(1.8f);
        onEXMoveColor = false;
        onAfterImageChange = false;

        yield break;
    }

    float calcDistObj()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.up / 2, transform.forward, out hit, 7, 1 << 10))
        {
            return hit.distance;
        }
        else
        {
            return 7;
        }
    }
}
