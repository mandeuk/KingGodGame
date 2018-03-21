using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMove : MonoBehaviour {
    public GameObject[] rendObjs = new GameObject[5];
    public GameObject[] afterImageRendObjs = new GameObject[5];
    public GameObject EXMovePos;

    GameObject EXMoveEffect;
    public GameObject afterImageR;

    float moveSpeed;
    bool onEXMove;
    bool onEXMoveColor;
    bool onAfterImageChange;
    bool onAfterImageColorChange;

    Animator avatar;
    Rigidbody raphaelRigidbody;

    Material transMat;
    //Material afterImageMat;

    Material[,] raphaelMats = new Material[5, 5];    // 라파엘 매터리얼 보관해두는 변수
    Material[,] afterImageMats = new Material[5, 5];

    //Material[] changeMats = new Material[10];


    // Use this for initialization
    void Awake () {
        raphaelRigidbody = transform.GetComponent<Rigidbody>();
        avatar = transform.GetComponent<Animator>();
        moveSpeed = transform.GetComponent<PlayerStatus>().moveSpeed;

        //afterImageMat = Resources.Load("Materials/AfterImageEffectMat") as Material;
        transMat = Resources.Load("Materials/Transparent") as Material;

        onEXMove = false;
        onEXMoveColor = false;
        onAfterImageChange = false;
        onAfterImageColorChange = false;

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
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            avatar.SetTrigger("EXMoveOn");
            onAfterImageChange = false;
            onEXMoveColor = false;
            onEXMove = false;
            StartCoroutine(EXMovePlay());
        }

        if (onAfterImageChange)
        {
            for (int i = 0; i < afterImageRendObjs.Length; ++i)
            {
                for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    afterImageRendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                            "_Color", Color.Lerp(afterImageRendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_Color"),
                            new Vector4(1, 1, 1, 0), Time.deltaTime * 7f));
                }
            }
        }

        if (onEXMove)
        {
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].SetActive(true);
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
                        raphaelMats[i, j].color, Time.deltaTime * 5f));
                }
            }
        }
    }

    public IEnumerator EXMovePlay()
    {
        
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].SetActive(false);
                rendObjs[i].GetComponent<Renderer>().materials[j].color = new Vector4(0, 0, 0, 0);
            }
        }


        afterImageR.transform.position = transform.position;
        afterImageR.GetComponent<Animator>().speed = 0;
        afterImageR.GetComponent<Rigidbody>().Sleep();

        for (int i = 0; i < afterImageRendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(true);
            for (int j = 0; j < afterImageRendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                afterImageRendObjs[i].GetComponent<Renderer>().materials[j].color = afterImageMats[i, j].color;
            }
        }

        
        onAfterImageChange = true;
        avatar.speed = 0;
        transform.GetComponent<PlayerStatus>().moveSpeed = 0;
        transform.GetComponent<PlayerEffect>().playEXMoveVanishEffect();

        yield return new WaitForSeconds(.15f);
        //transform.GetComponent<PlayerEffect>().playEXMoveSlashEffect();
        for (int i = 0; i < afterImageRendObjs.Length; ++i)
        {
            afterImageRendObjs[i].SetActive(false);
        }
        onEXMoveColor = true;
        onEXMove = true;
        //raphaelRigidbody.MovePosition(transform.position + transform.forward);
        transform.position += transform.forward * 3;
        transform.GetComponent<PlayerStatus>().moveSpeed = moveSpeed;
        
        avatar.speed = 1;
        afterImageR.GetComponent<Animator>().speed = 1;
        

        yield return new WaitForSeconds(.4f);
        onEXMove = false;
        

        yield return new WaitForSeconds(1f);
        onEXMoveColor = false;
        onAfterImageChange = false;

        yield break;

    }
}
