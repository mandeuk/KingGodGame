using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public static ItemManager instance;

    public List<GameObject> itemList = new List<GameObject>(); //아이템풀 역할을 할 리스트 선언
    public List<GameObject> useditemList = new List<GameObject>(); //맵에 스폰한 아이템을 저장할 리스트 선언

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < 10; ++i)
        {
            itemList.Add(GenerateItem());//Item을 생성해서 넣어야 함
        }

        for (int i = 0; i < 20; ++i)
            SpawnItem();
    }
    

    public GameObject GenerateItem()
    {
        GameObject cloneItem = Instantiate(Resources.Load("Prefabs/Item")) as GameObject;//Items 생성
        cloneItem.SetActive(false);

        return cloneItem;
    }
    
    public void SpawnItem()
    {
        //아이템풀에 남아있는 아이템이 없을 경우 추가로 생성
        if(itemList.Count == 0)
            itemList.Add(GenerateItem());

        useditemList.Add(itemList[0]);//지도에 스폰된 아이템을 useditemList에 추가
        
        //스폰시킬 아이템의 좌표 설정 및 오브젝트 활성화
        itemList[0].transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10),1, Random.Range(-10, 10)),new Quaternion(0,0,0,1));
        itemList[0].SetActive(true);

        //아이템풀에서 삭제
        itemList.Remove(itemList[0]);//지도에 스폰된 아이템을 itemList에서 삭제
    }

    public void GetItem(GameObject itemObj)
    {
        //GameObject newobject = new GameObject();
        if(useditemList.Remove(itemObj))
        {
            itemList.Add(itemObj);
            Debug.Log("아이템 먹음!");
        }
        else
        {
            Debug.Log("아이템을 usedList에서 삭제하지 못함");
        }
    }
    
}