using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    
    public List<GameObject> slashcloneList = new List<GameObject>();
    public List<GameObject> usedslashcloneList = new List<GameObject>();
    public GameObject EffectPos;

    public IEnumerator PlayEffect()
    {
        usedslashcloneList.Add(slashcloneList[0]);
        slashcloneList[0].SetActive(true);
        slashcloneList[0].transform.position = EffectPos.transform.position;
        slashcloneList[0].transform.rotation = 
            Quaternion.Euler(-270, 180 + transform.rotation.eulerAngles.y, 20);

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
