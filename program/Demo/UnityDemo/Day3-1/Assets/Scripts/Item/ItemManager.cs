using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    private static ItemManager instance = null;

    public List<GameObject> itemList = new List<GameObject>(); //아이템풀 역할을 할 리스트 선언
    public List<GameObject> useditemList = new List<GameObject>(); //맵에 스폰한 아이템을 저장할 리스트 선언

    public static ItemManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        //아이템매니저를 싱글턴 오브젝트로 만들어 씬이 변경되어도 삭제되지 않고 살아서 아이템을 관리하도록 해줍니다.
        if (instance)//인스턴스가 생성되어있는가?
        {
            DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }


        //빈 아이템을 미리 생성해서 메모리를 미리 확보해놓습니다.
        for (int i = 0; i < 10; ++i)
        {
            itemList.Add(GenerateItem());//Item을 생성해서 넣어야 함
        }
        
    }



    // 함수이름 : GameObject GenerateItem()
    // 기능 : 아이템을 생성하지만, 바로 맵에 배치하지 않고 메모리에 공간만 확보하는 기능
    public GameObject GenerateItem()
    {
        GameObject cloneItem = Instantiate(Resources.Load("Prefabs/Item")) as GameObject;//Items 생성
        cloneItem.SetActive(false);

        return cloneItem;
    }
    


    // 함수이름 : void SpawnItem()
    // 기능 : 생성한 아이템이 저장된 아이템풀에서 아이템을 꺼내 맵에 배치한다.
    public void SpawnItem(Vector3 itempos)
    {
        //아이템풀에 남아있는 아이템이 없을 경우 추가로 생성
        if(itemList.Count == 0)
            itemList.Add(GenerateItem());

        useditemList.Add(itemList[0]);//지도에 스폰된 아이템을 useditemList에 추가

        //스폰시킬 아이템의 좌표 설정 및 오브젝트 활성화
        //itemList[0].transform.SetPositionAndRotation(new Vector3(Random.Range(-10, 10),1, Random.Range(-10, 10)),new Quaternion(0,0,0,1));
        itempos.y += 0.7f;//아이템이 땅바닥에 박히지 않게 하기 위한 y값 증가
        itemList[0].transform.SetPositionAndRotation(itempos, new Quaternion(0, 0, 0, 1));
        
        itemList[0].SetActive(true);

        //아이템풀에서 삭제
        itemList.Remove(itemList[0]);//지도에 스폰된 아이템을 itemList에서 삭제
    }



    // 함수이름 : void GetItem(GameObject itemObj)
    // 기능 : 아이템이 캐릭터와 충돌했을 때 아이템을 획득하고 획득한 아이템 오브젝트는 다시 아이템풀로 집어넣는다.
    // 첨언 : 충돌체크는 Item.cs파일 내부에서 OnTriggerEnter()함수를 사용하여 체크한 후 GetItem()함수를 호출한다.
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