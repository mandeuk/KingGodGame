using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    
    public List<GameObject> slashcloneList = new List<GameObject>();
    public List<GameObject> usedslashcloneList = new List<GameObject>();

    public GameObject blinkclone;
    public GameObject slashRingEffect;
    public GameObject EXMoveSlashEffect;
    public GameObject EXmoveVanishEffect;

    public GameObject Effect1Pos;
    public GameObject Effect2Pos;
    public GameObject Effect3Pos;
    public GameObject Effect4Pos;
    public GameObject blinkEffectPos;


    public IEnumerator PlayEffect(int stateNum)
    {
        //state = transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);    
        // 이거 first 어택은 idle 다음에 읽히지 않고                                    
        // second는 아예안읽힘   state.IsName("Base Layer.attack_second")
        // 왜인지 모르겠음;;;;;;

        //slashRingEffect.SetActive(true);
        //slashRingEffect.transform.position =
        //    new Vector3(transform.position.x, slashRingEffect.transform.position.y, transform.position.z);
        
        //slashRingEffect.GetComponent<ParticleSystem>().Play();

        usedslashcloneList.Add(slashcloneList[0]);
        slashcloneList[0].SetActive(true);
        slashcloneList[0].transform.localScale = new Vector3(1f, 1f, 1f);

        if (stateNum == 1)
        {
            slashcloneList[0].transform.position = Effect1Pos.transform.position;
            slashcloneList[0].transform.rotation =
                Quaternion.Euler(-90, 180 + transform.rotation.eulerAngles.y, 205);
        }

        else if (stateNum == 2)
        {
            slashcloneList[0].transform.position = Effect2Pos.transform.position;
            slashcloneList[0].transform.rotation =
                Quaternion.Euler(-110-180, 270 + transform.rotation.eulerAngles.y, 110);
        }

        else if (stateNum == 3)
        {
            slashcloneList[0].transform.position = Effect3Pos.transform.position;
            slashcloneList[0].transform.rotation =
                Quaternion.Euler(101-180, 270 + transform.rotation.eulerAngles.y, 110);
        }

        else if (stateNum == 4)
        {
            slashcloneList[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            slashcloneList[0].transform.position = Effect4Pos.transform.position;
            slashcloneList[0].transform.rotation =
                Quaternion.Euler(-90, 180 + transform.rotation.eulerAngles.y, 205);
        }

        slashcloneList.RemoveAt(0);
        

        yield return new WaitForSeconds(1f);

        slashcloneList.Add(usedslashcloneList[0]);
        usedslashcloneList[0].SetActive(false);
        usedslashcloneList.RemoveAt(0);

        yield break;
    }

    public void playBlinkEffect()
    {
        if (transform.CompareTag("Player"))
        {
            blinkclone.SetActive(true);
            blinkclone.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playEXMoveSlashEffect()
    {
        if (transform.CompareTag("Player"))
        {
            EXMoveSlashEffect.SetActive(true);
            EXMoveSlashEffect.transform.position = transform.position + Vector3.forward * 4.5f;
            EXMoveSlashEffect.transform.rotation = Quaternion.Euler(0, 90 + transform.rotation.eulerAngles.y, 0);
            EXMoveSlashEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playEXMoveVanishEffect()
    {
        if (transform.CompareTag("Player"))
        {
            EXmoveVanishEffect.SetActive(true);
            EXmoveVanishEffect.transform.position = transform.position;
            EXmoveVanishEffect.transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
            EXmoveVanishEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    // Use this for initialization
    void Awake() {
        blinkclone = Instantiate(Resources.Load("Prefabs/Effect/BlinkEffect"), blinkEffectPos.transform) as GameObject;
        //slashRingEffect = Instantiate(Resources.Load("Prefabs/Effect/slashRingEffect")) as GameObject;
        EXMoveSlashEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveSlashEffectSimple"), blinkEffectPos.transform) as GameObject;
        EXmoveVanishEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveVanishEffect"), blinkEffectPos.transform) as GameObject;
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
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/slash1")) as GameObject;

        return effectClone;
    }
}
