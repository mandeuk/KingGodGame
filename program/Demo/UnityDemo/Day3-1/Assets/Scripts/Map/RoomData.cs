using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour {
    public bool isClear;
    public bool playerIn;
    public int x, y;
    public GameObject meshs;
    

    public int[,] mapSpawnArray = new int[9, 9];
    public List<GameObject> EnemyClones = new List<GameObject>();

    // Use this for initialization
    void Awake () {
        isClear = false;
        playerIn = false;
        //meshs.SetActive(false);
    }

    private void Start()
    {
        EnemyClones = transform.GetComponentInChildren<ObstacleData>().EnemyClones;
        mapSpawnArray = StageManager.instance.mapSpawnArray;
        RoomWait();
    }

    // Update is called once per frame
    // 이거 꼭 업데이트에서 불러야할까? 매프레임 이렇게..???? 후우..
    void Update () {
        // 룸의 상태를 판단. 
        if (playerIn)
        {
            //meshs.SetActive(true);  // 프레임을 잡아 먹으려나..? 따로 함수를 만들어서 한번만  호출되게 할까..?

            // 클리어가 되어있지 않고 플레이어가 들어온 상태 -> 룸 Start
            if (!isClear)
            {
                RoomStart();

                // 클리어 여부 판단.
                if (EnemyClones.Count < 1)
                {
                    RoomClear();
                }
            }

            // 클리어가 되어있고 플레이어가 들어온 상태
            else
            {
                GetComponentInChildren<RoomColManager>().JudgePlayerInNextRoom();
            }
        }

        // 클리어가 되어있지 않고 플레이어도 안들어온 상태 -> 룸 Wait // 이거는 근데 매프레임 불릴 필요가 없음.
        //else if (!playerIn && !isClear)
        //{
        //    RoomWait();
        //}   
    }   

    // 룸을 클리어한 상태에서 호출
    public void RoomClear()
    {
        isClear = true;

        // 문이 열려야함
        transform.GetComponent<Animator>().SetBool("DoorOpen", true);
        // 콜라이더가 바뀌어야함
        JudgeRoomDoorOpen();
    }

    // 룸에 플레이어가 들어왔을때 시작하는 상태에서 호출
    public void RoomStart()
    {
        transform.GetComponent<Animator>().SetBool("DoorOpen", false);
    }

    // 룸이 스폰되고 플레이어가 진입전의 상태에서 호출
    public void RoomWait()
    {
        transform.GetComponent<Animator>().SetBool("DoorOpen", true);
    }

    // 룸이 클리어 된 상태면 플레이어가 있건 없건  Room은 Open임. 그때 호출.
    public void RoomOpen()
    {

    }

    void JudgeRoomDoorOpen()
    {
        if (mapSpawnArray[x + 1, y] > 0)
        {
            GetComponentInChildren<RoomColManager>().LeftDoorOpen();
        }
        if (mapSpawnArray[x - 1, y] > 0)
        {
            GetComponentInChildren<RoomColManager>().RightDoorOpen();
        }
        if (mapSpawnArray[x, y + 1] > 0)
        {
            GetComponentInChildren<RoomColManager>().FrontDoorOpen();
        }
        if (mapSpawnArray[x, y - 1] > 0)
        {
            GetComponentInChildren<RoomColManager>().BackDoorOpen();
        }
    }
}
