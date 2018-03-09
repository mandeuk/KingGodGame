using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour {

    public List<GameObject> hitcloneList = new List<GameObject>();
    public List<GameObject> usedhitcloneList = new List<GameObject>();

    public IEnumerator PlayEffect(int stateNum)
    {
        usedhitcloneList.Add(hitcloneList[0]);
        hitcloneList[0].SetActive(true);
        hitcloneList[0].transform.position = transform.position + Vector3.up*0.7f;
        hitcloneList[0].transform.rotation = transform.rotation;

        //if (stateNum == 1)
        //{
        //    hitcloneList[0].transform.rotation =
        //        Quaternion.Euler(90, 180 + transform.rotation.eulerAngles.y, 20);
        //}

        //else if (stateNum == 2)
        //{
        //    hitcloneList[0].transform.rotation =
        //        Quaternion.Euler(-110, 270 + transform.rotation.eulerAngles.y, 110);
        //}

        //else if (stateNum == 3)
        //{
        //    hitcloneList[0].transform.rotation =
        //        Quaternion.Euler(101, 270 + transform.rotation.eulerAngles.y, 110);
        //}

        //else if (stateNum == 4)
        //{
        //    hitcloneList[0].transform.rotation =
        //        Quaternion.Euler(90, 180 + transform.rotation.eulerAngles.y, 20);
        //}

        hitcloneList.RemoveAt(0);


        yield return new WaitForSeconds(1f);

        hitcloneList.Add(usedhitcloneList[0]);
        usedhitcloneList[0].SetActive(false);
        usedhitcloneList.RemoveAt(0);

        yield break;
    }

    // Use this for initialization
    void Awake()
    {
        InitEffect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitEffect()
    {
        for (int i = 0; i < 10; ++i)
            hitcloneList.Add(SpawnEffect());
    }

    public GameObject SpawnEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/SlashHit")) as GameObject;

        return effectClone;
    }
}