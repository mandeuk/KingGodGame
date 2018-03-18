using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMove : MonoBehaviour {
    public GameObject[] rendObjs = new GameObject[5];
    public GameObject EXMovePos;
    float moveSpeed;
    bool onEXMove;

    Animator avatar;
    Rigidbody raphaelRigidbody;
    Material transMat;
    Material afterImageMat;
    List<Material> raphaelList = new List<Material>();
    Material[,] raphaelMats = new Material[5,5];
    Material[] changeMats;


    // Use this for initialization
    void Awake () {
        onEXMove = false;
        raphaelRigidbody = transform.GetComponent<Rigidbody>();
        avatar = transform.GetComponent<Animator>();
        moveSpeed = transform.GetComponent<PlayerMovement>().moveSpeed;

        afterImageMat = Resources.Load("Materials/AfterImageEffectMat") as Material;
        transMat = Resources.Load("Materials/Transparent") as Material;

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                raphaelMats[i,j] = Instantiate(rendObjs[i].GetComponent<Renderer>().materials[j]) as Material;
            }
        }

        // 메터리얼을 교체하기 위한거였는데 이제 필요없음.
        // 이제 무조건 배열안에 넣을때는 인스턴스 해서 넣기 알았지? ㅅㅂ

        //for (int i = 0; i < rendObjs.Length; ++i)
        //{
        //    for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
        //    {
        //        changeMats = rendObjs[i].GetComponent<Renderer>().materials;
        //        changeMats[j] = raphaelMats[i, j];
        //        rendObjs[i].GetComponent<Renderer>().materials = changeMats;
        //    }
        //}

        // 메터리얼이 2개이상있는 메쉬를 바꾸는 방법인 코드.
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(EXMovePlay());
        }
        if (onEXMove)
        {
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].SetActive(true);
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                        "_Color", Color.Lerp(rendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_Color"),
                        raphaelMats[i, j].color, Time.deltaTime * 2));
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
        avatar.speed = 0;
        transform.GetComponent<PlayerMovement>().moveSpeed = 0;
        raphaelRigidbody.Sleep();
        raphaelRigidbody.velocity = new Vector3(0,0,0);

        yield return new WaitForSeconds(.5f);
        transform.position = EXMovePos.transform.position;
        onEXMove = true;
        transform.GetComponent<PlayerMovement>().moveSpeed = moveSpeed;
        avatar.SetTrigger("EXMoveOn");
        avatar.speed = 1;

        

        yield return new WaitForSeconds(1.5f);
        onEXMove = false;

        yield break;

    }
}
