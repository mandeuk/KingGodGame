using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerStatus
{
    public float energy, attackSpeed, attackRange, devilGage, etere, stance;
    public float maxHP, curHP, attackPower, moveSpeed, pushBack;
}

[Serializable]
public class PlayerPosition
{
    public int curRoomX, curRoomY, pastRoomX, pastRoomY, stageNum;
}

[Serializable]
public class PlayerItemList
{
    public List<ItemNum> equipItems = new List<ItemNum>();
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    static PlayerStatus playerStatus;       //  플레이어 스테이터스와 플레이어 아이템 둘다 스태틱으로 선언함.
    static PlayerItemList playerItemList;   //  둘다 읽어서 상태창에 표시해야함.
    public static PlayerPosition playerPosition;

    public bool testMod;

    static string statusFilePathStatus = string.Empty;
    static string itemFilePathStatus = string.Empty;

    // Use this for initialization
    void Awake()
    {
        testMod = true;
        statusFilePathStatus = Application.dataPath + "/Status.bin";
        itemFilePathStatus = Application.dataPath + "/item.bin";

        if (instance) //인스턴스가 생성되어있는가?
        {
            DestroyImmediate(gameObject); //생성되어있다면 중복되지 않도록 삭제
            return;
        }

        else //인스턴스가 null일 때
        {
            instance = this; //인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            DontDestroyOnLoad(gameObject); //씬이 바뀌어도 계속 유지하도록 설정

            NewGameStart(); // 임시로 이렇게 해둠 나중에 메뉴씬을 정확히 나누게 되면 지우고 버튼에 장착
        }
    }

    // 함수 기능 : 새로운 게임을 시작할 때 불림
    public static void NewGameStart()
    {
        playerStatus = new PlayerStatus
        {
            maxHP = 8,      // 맥스 hp
            curHP = 8,      // 현재 hp
            attackPower = 30,     // 공격력
            attackSpeed = 1f,      // 공격속도
            attackRange = 1,      // 공격 범위
            moveSpeed = 8,      // 이동 속도
            pushBack = 5,      // 넉백을 주는 힘
            energy = 3,      // 에너지
            etere = 0,      // 에테르
            devilGage = 25,     // 폭주 게이지 100이되면 죽음.
            stance = 0      // 성향. 구원,타락 수치로 성향이 높아지고 낮아진다. 
        };

        playerItemList = new PlayerItemList();
        playerPosition = new PlayerPosition
        {
            stageNum = 1
        };

        PlayerStatusSave(playerStatus);
        PlayerItemSave(ItemNum.Empty);
    }

    // 함수 기능 :  기존 게임의 데이터를 불러옴
    //              데이터는 어짜피 플레이어베이스에서 가져갈거임
    public static void LoadGameData()
    {
        //PlayerStatusLoad();
    }

    public static void PlayerStatusSave(PlayerStatus status)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(statusFilePathStatus, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(stream, status);
        stream.Close();
    }

    public static PlayerStatus PlayerStatusLoad()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(statusFilePathStatus, FileMode.Open, FileAccess.Read);
        PlayerStatus playerstatus = (PlayerStatus)formatter.Deserialize(stream);

        return playerstatus;
    }

    public static void PlayerItemSave(ItemNum item)
    {
        playerItemList.equipItems.Add(item);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(itemFilePathStatus, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(stream, playerItemList);
        stream.Close();
    }

    public static void PlayerItemLoad()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(itemFilePathStatus, FileMode.Open, FileAccess.Read);
        playerItemList = (PlayerItemList)formatter.Deserialize(stream);

        // 이걸 여기서 풀어야하나싶지만 일단 여기서 읽은 파일을 읽어서 패키징푸는ㄴ걸로..
        // 치트아이템 고려해야됨 막만들어둔거라. 치트아이템도 먹힐거임 여기서
        foreach (ItemNum item in playerItemList.equipItems)
        {
            if (item.CompareTo(ItemNum.LutusOfAbyss) == 0)
            {
                Itemtable.Instance.SpawnLotusOfAbyss();
            }
            else if (item.CompareTo(ItemNum.SmallSword) == 0)
            {
                Itemtable.Instance.SpawnSmallSword();
            }
            else if (item.CompareTo(ItemNum.SoulOfImp) == 0)
            {
                Itemtable.Instance.SpawnSoulOfImp();
            }
        }
    }

    public static void StageClear(Vector3 roomPos)
    {
        EffectManager.instance.stageClearDoorEffect(roomPos);
        SoundManager.instance.StopStageBGM();
        PlayerBase.instance.EndDevil();
    }

    public void MoveStage()
    {
        StartCoroutine(StageMoveCo());
    }

    IEnumerator StageMoveCo()
    {
        PlayerBase.instance.GetComponent<PlayerMovement>().moveRoom = true;
        PlayerBase.instance.PlayerDisable();
        PlayerBase.instance.GetComponent<Animator>().SetTrigger("roomMove");   
        SoundManager.instance.StopEnvironmentalBGM();
        AsyncOperation sceneApply = new AsyncOperation();

        PlaySceneUIManager.instance.UIFadeOut();

        if (playerPosition.stageNum == 1)
        {
            playerPosition.stageNum = 2;
            sceneApply = SceneManager.LoadSceneAsync("Game_Stage2");
        }
        else if (playerPosition.stageNum == 2)
        {
            playerPosition.stageNum = 1;
            sceneApply = SceneManager.LoadSceneAsync("GameIntromenu");
        }
        sceneApply.allowSceneActivation = false;

        yield return new WaitForSecondsRealtime(4.0f);
        sceneApply.allowSceneActivation = true;

        //yield return new WaitForSecondsRealtime(1.0f);

        //if (playerPosition.stageNum == 1)
        //{
        //    playerPosition.stageNum = 2;            
        //    SceneManager.LoadScene("Game_Stage2");
        //}

        //// 임시로 로딩으로넘어가게함 스테이지3이 없어서..
        //else if (playerPosition.stageNum == 2)
        //{
        //    playerPosition.stageNum = 1;
        //    SceneManager.LoadScene("GameIntromenu");
        //}

        yield break;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerBase.instance.EndDevil();
            MoveStage();
        }
    }
}
