using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    
    public List<GameObject> slashcloneList = new List<GameObject>();
    public List<GameObject> usedslashcloneList = new List<GameObject>();
    public GameObject Effect1Pos;
    public GameObject Effect2Pos;
    public GameObject Effect3Pos;
    public GameObject Effect4Pos;
    AnimatorStateInfo state;


    public IEnumerator PlayEffect(int stateNum)
    {
        //state = transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);    
        // 이거 first 어택은 idle 다음에 읽히지 않고                                    
        // second는 아예안읽힘   state.IsName("Base Layer.attack_second")
        // 왜인지 모르겠음;;;;;;

        usedslashcloneList.Add(slashcloneList[0]);
        slashcloneList[0].SetActive(true);

        //slashcloneList[0].transform.position = EffectPos.transform.position;
        //slashcloneList[0].transform.position = transform.position;

        if (stateNum == 1)
        {
            slashcloneList[0].transform.position = Effect1Pos.transform.position;
            slashcloneList[0].transform.rotation =
            Quaternion.Euler(90, 180 + transform.rotation.eulerAngles.y, 20);
        }

        else if (stateNum == 2)
        {
            slashcloneList[0].transform.position = Effect2Pos.transform.position;
            slashcloneList[0].transform.rotation =
            Quaternion.Euler(-110, 270 + transform.rotation.eulerAngles.y, 110);
        }

        else if (stateNum == 3)
        {
            slashcloneList[0].transform.position = Effect3Pos.transform.position;
            slashcloneList[0].transform.rotation =
            Quaternion.Euler(101, 270 + transform.rotation.eulerAngles.y, 110);
        }

        else if (stateNum == 4)
        {
            slashcloneList[0].transform.position = Effect4Pos.transform.position;
            slashcloneList[0].transform.rotation =
            Quaternion.Euler(90, 180 + transform.rotation.eulerAngles.y, 20);
        }

        slashcloneList.RemoveAt(0);
        

        yield return new WaitForSeconds(1f);

        slashcloneList.Add(usedslashcloneList[0]);
        usedslashcloneList[0].SetActive(false);
        usedslashcloneList.RemoveAt(0);

        yield break;
    }

    // Use this for initialization
    void Awake() {
        InitEffect();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void InitEffect()
    {
        for (int i = 0; i < 10; ++i)
            slashcloneList.Add(SpawnEffect());
    }

    public GameObject SpawnEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/slash2")) as GameObject;

        return effectClone;
    }
}
