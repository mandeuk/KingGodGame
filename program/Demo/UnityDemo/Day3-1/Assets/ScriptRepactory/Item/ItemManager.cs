using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    private static ItemManager instance = null;
    public GameObject player;

    public List<GameObject> itemList = new List<GameObject>(); //아이템풀 역할을 할 리스트 선언
    public List<GameObject> useditemList = new List<GameObject>(); //맵에 스폰한 아이템을 저장할 리스트 선언

    public GameObject ItemCloneList;

    public static ItemManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        player = PlayerBase.instance.gameObject;
    }

    private void Start()
    {
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
        GameObject cloneItem = Instantiate(Resources.Load("Prefabs/Item"), ItemCloneList.transform) as GameObject;//Items 생성
        cloneItem.SetActive(false);

        return cloneItem;
    }

    // 함수이름 : void SpawnItem()
    // 기능 : 생성한 아이템이 저장된 아이템풀에서 아이템을 꺼내 맵에 배치한다.
    public void SpawnItem(Vector3 itempos, int itemtype)
    {
        //아이템풀에 남아있는 아이템이 없을 경우 추가로 생성
        if(itemList.Count == 0)
            itemList.Add(GenerateItem());

        useditemList.Add(itemList[0]);//지도에 스폰된 아이템을 useditemList에 추가

        //아이템 속성 설정
        itemList[0].GetComponent<Item>().SetupItem(itemtype);

        //스폰시킬 아이템의 좌표 설정 및 오브젝트 활성화
        itempos.y += 1.7f;//아이템이 땅바닥에 박히지 않게 하기 위한 y값 증가
        itemList[0].transform.SetPositionAndRotation(itempos, new Quaternion(0, 0, 0, 1));
        itemList[0].SetActive(true);
        itemList[0].GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000.0f, 0));

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
            //Debug.Log("아이템 먹음!");
        }
        else
        {
            //Debug.Log("아이템을 usedList에서 삭제하지 못함");
        }
    }

    //  함수 기능 : 아이템을 스폰함.
    //              나중에 델리게이트로 쓰던 할거기때문에 static 안붙일거고
    //              일단 아무렇게나 사용할거임. 나중에 리팩토링.
    public void SpawnSmallSwordItem(Vector3 pos)
    {
        GameObject itemClone 
            = Instantiate(Resources.Load("Prefabs/Item/SmallSword/SmallSwordItemEffect"), transform) as GameObject;
        itemClone.transform.position = pos + Vector3.up * 1.5f;
    }

    public void SpawnLotusOfAbyssItem(Vector3 pos)
    {
        GameObject itemClone
            = Instantiate(Resources.Load("Prefabs/Item/LotusOfAbyss/LotusOfAbyssItemEffect"), transform) as GameObject;
        itemClone.transform.position = pos + Vector3.up * 1.5f;
    }

    public void SpawnSoulOfImpItem(Vector3 pos)
    {
        GameObject itemClone
            = Instantiate(Resources.Load("Prefabs/Item/SoulOfImp/SoulOfImpItemEffect"), transform) as GameObject;
        itemClone.transform.position = pos + Vector3.up * 1.5f;
    }
}