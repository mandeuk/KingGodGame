using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public List<GameObject> itemList = new List<GameObject>(); //아이템을 저장할 리스트 선언
    public List<GameObject> useditemList = new List<GameObject>(); //맵에 스폰한 아이템을 저장할 리스트 선언


    private void Awake()
    {
        for (int i = 0; i < 10; ++i)
        {
            itemList.Add(GenerateItem());//Item을 생성해서 넣어야 함
        }

        SpawnItem();
    }
    

    public GameObject GenerateItem()
    {
        GameObject cloneItem = Instantiate(Resources.Load("Prefabs/Item")) as GameObject;//Items 생성

        cloneItem.SetActive(false);


        return cloneItem;
    }
    
    void SpawnItem()
    {
        useditemList.Add(itemList[0]);//지도에 스폰된 아이템을 useditemList에 추가

        //Instantiate(itemList[0]);
        Vector3 vec;
        vec.x = 100;
        vec.y = 100;
        vec.z = 100;
        Quaternion qua;
        qua.x = 0;
        qua.y = 0;
        qua.z = 0;
        qua.w = 1;
        //itemList[0].transform.SetPositionAndRotation(vec, qua);
        itemList[0].transform.SetPositionAndRotation(new Vector3(10,1,10),new Quaternion(0,0,0,1));
        //itemList[0].transform.position = EnemyDiePosition;

        itemList[0].SetActive(true);

        itemList.Remove(itemList[0]);//지도에 스폰된 아이템을 itemList에서 삭제
    }
    
    
    
}