using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomData : MonoBehaviour {
    public bool isClear, isFind, playerIn;
    public int x, y, roomNumber;    

    public GameObject fadeCamera;
    public GameObject[] roomPos = new GameObject[4];
    Transform player;
    //GameObject virtualCam;
    RoomSpawn roomSpawn;

    public GameObject[] FluidFire = new GameObject[4];

    public int[,] mapSpawnArray = new int[9, 9];
    public List<GameObject> EnemyClones = new List<GameObject>();

    // Use this for initialization
    void Awake () {
        //virtualCam = GameObject.FindWithTag("MainVirtualCamera");
        //player = GameObject.FindWithTag("Player").transform;
        player = PlayerBase.instance.transform;
        isClear = false;
        isFind = false;
        playerIn = false;
        //EnemyClones = transform.parent.GetComponentInChildren<ObstacleData>().EnemyClones;
        roomSpawn = RoomSpawn.instance;
    }

    private void Start()
    {
        EnemyClones = transform.parent.GetComponentInChildren<ObstacleData>().EnemyClones;

        mapSpawnArray = StageManager.instance.mapSpawnArray;
        if(!GameManager.instance.testMod)
            JudgeFluidFire();
    }

    // Update is called once per frame
    // 이거 꼭 업데이트에서 불러야할까? 매프레임 이렇게..???? 후우..
    void Update () {
        // 룸의 상태를 판단. 
        if (playerIn)
        {            
            // 클리어가 되어있지 않고 플레이어가 들어온 상태 -> 룸 Start
            if (!isClear)
            {
                // 클리어 여부 판단.
                if (EnemyClones.Count < 1)
                {
                    RoomClear();
                }
            }
        }
    }   

    // 룸을 클리어한 상태에서 호출
    public void RoomClear()
    {
        isClear = true;

        // 문이 열려야함
        if (transform.GetComponent<Animator>().enabled)
            transform.GetComponent<Animator>().SetBool("DoorOpen", true);
        JudgeFluidFireOpen();

        // 콜라이더가 바뀌어야함
        JudgeRoomDoorOpen();

        if(!(roomNumber == 2))
        {
            SoundManager.playDoorOpen();
        }

        if (roomNumber == 6)
        {
            SoundManager.playItemDropSound();
            ItemManager.Instance.SpawnSmallSwordItem(transform.position);
        }

        else if(roomNumber == 4)
        {
            SoundManager.playItemDropSound();
            ItemManager.Instance.SpawnSoulOfImpItem(transform.position);
        }

        else if(roomNumber == 5)
        {
            SoundManager.playItemDropSound();
            ItemManager.Instance.SpawnLotusOfAbyssItem(transform.position);
        }

        else if(roomNumber == 8)
        {
            StartCoroutine(ClearDoorApearCo(transform.position));
        }

        else if(roomNumber == 9)
        {
            StartCoroutine(ClearDoorApearCo(transform.position));
        }

        else if(roomNumber == 11)
        {
            ItemManager.Instance.SpawnCheatItem(transform.position);
        }
    }

    IEnumerator ClearDoorApearCo(Vector3 pos)
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.StageClear(pos);
        yield break;
    }

    // 룸에 플레이어가 들어왔을때 시작하는 상태에서 호출
    public void RoomStart()
    {
        playerIn = true;
        if (!isClear)
        {
            if (transform.GetComponent<Animator>().enabled)
                transform.GetComponent<Animator>().SetBool("DoorOpen", false);

            if (roomNumber == 5)
            {
                BossLifebarManager.instance.ActivateBosslifeGauge();
            }

            if (roomNumber == 8)
            {
                SoundManager.instance.playCorosusMusic();
                BossLifebarManager.instance.ActivateBosslifeGauge();
            }

            else if (roomNumber == 9)
            {
                SoundManager.instance.playHighPriestMusic();
                BossLifebarManager.instance.ActivateBosslifeGauge();
            }
        }        
    }    

    public IEnumerator RoomMove(int doorType)
    {
        player.GetComponent<PlayerMovement>().moveRoom = true;
        player.GetComponent<Animator>().SetTrigger("roomMove");
        CameraFadeInOut.instance.FadeOut();
        //virtualCam.SetActive(false);

        yield return new WaitForSeconds(0.8f);
        playerGoNextRoom(doorType);
        //cameraFade.transform.position = player.transform.position;
        //Camera.main.transform.position = player.transform.position;

        yield return new WaitForSeconds(0.8f);
        CameraFadeInOut.instance.FadeIn();

        yield return new WaitForSeconds(0.1f);
        //virtualCam.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        if (!isClear)
        {
            SoundManager.playDoorClose();
        }

        yield return new WaitForSeconds(0.5f);
        playerIn = false;
        player.GetComponent<PlayerMovement>().moveRoom = false;
        transform.parent.gameObject.SetActive(false); // 마지막으로 내가 잇었던 방의 active를 꺼줌

        PlaySceneUIManager.instance.UpdateMap();//코드추가 : 이인호, 맵을 옮길때마다 미니맵과 전체맵 갱신해주기 위한 코드
        yield break;
    }

    void JudgeRoomDoorOpen()
    {
        if (mapSpawnArray[y, x + 1] > 0)
        {
            transform.parent.GetComponentInChildren<RoomColManager>().LeftDoorOpen();
        }
        if (mapSpawnArray[y, x - 1] > 0)
        {
            transform.parent.GetComponentInChildren<RoomColManager>().RightDoorOpen();
        }
        if (mapSpawnArray[y + 1, x] > 0)
        {
            transform.parent.GetComponentInChildren<RoomColManager>().FrontDoorOpen();
        }
        if (mapSpawnArray[y - 1, x] > 0)
        {
            transform.parent.GetComponentInChildren<RoomColManager>().BackDoorOpen();
        }
    }

    public void JudgeFluidFireOpen()
    {
        if (mapSpawnArray[y, x + 1] > 0)
        {
            FluidFire[0].GetComponent<FluidFireAction>().DoorOpen();
        }
        else
        {
            FluidFire[0].GetComponent<FluidFireAction>().DoorEmpty();
        }

        if (mapSpawnArray[y, x - 1] > 0)
        {
            FluidFire[2].GetComponent<FluidFireAction>().DoorOpen();
        }
        else
        {
            FluidFire[2].GetComponent<FluidFireAction>().DoorEmpty();
        }

        if (mapSpawnArray[y + 1, x] > 0)
        {
            FluidFire[1].GetComponent<FluidFireAction>().DoorOpen();
        }
        else
        {
            FluidFire[1].GetComponent<FluidFireAction>().DoorEmpty();
        }

        if (mapSpawnArray[y - 1, x] > 0)
        {
            FluidFire[3].GetComponent<FluidFireAction>().DoorOpen();
        }
        else
        {
            FluidFire[3].GetComponent<FluidFireAction>().DoorEmpty();
        }
    }

    public void JudgeFluidFire()
    {
        if (mapSpawnArray[y, x + 1] > 0)
        {
            FluidFire[0].GetComponent<FluidFireAction>().DoorClose();
        }
        else
        {
            FluidFire[0].GetComponent<FluidFireAction>().DoorEmpty();
        }

        if (mapSpawnArray[y, x - 1] > 0)
        {
            FluidFire[2].GetComponent<FluidFireAction>().DoorClose();
        }
        else
        {
            FluidFire[2].GetComponent<FluidFireAction>().DoorEmpty();
        }

        if (mapSpawnArray[y + 1, x] > 0)
        {
            FluidFire[1].GetComponent<FluidFireAction>().DoorClose();
        }
        else
        {
            FluidFire[1].GetComponent<FluidFireAction>().DoorEmpty();
        }

        if (mapSpawnArray[y - 1, x] > 0)
        {
            FluidFire[3].GetComponent<FluidFireAction>().DoorClose();
        }
        else
        {
            FluidFire[3].GetComponent<FluidFireAction>().DoorEmpty();
        }
    }

    public void playerGoNextRoom(int doorType)
    {
        if(doorType == 0)
        {
            roomSpawn.mapDataArray[y, x + 1].transform.parent.gameObject.SetActive(true);

            player.position 
                = roomSpawn.mapDataArray[y, x + 1].GetComponent<RoomData>().roomPos[doorType + 2].transform.position;
            player.GetComponent<PlayerBase>().curRoomPoint = new Vector2(y, x + 1);
            roomSpawn.mapDataArray[y, x + 1].GetComponent<RoomData>().playerIn = true;
            roomSpawn.mapDataArray[y, x + 1].GetComponent<RoomData>().RoomStart();            

            StormCloudPosition.instance.transform.position = roomSpawn.mapDataArray[y, x + 1].transform.position - Vector3.up * 8;
        }
        else if (doorType == 1)
        {
            roomSpawn.mapDataArray[y + 1, x].transform.parent.gameObject.SetActive(true);

            player.position
                = roomSpawn.mapDataArray[y + 1, x].GetComponent<RoomData>().roomPos[doorType + 2].transform.position;
            player.GetComponent<PlayerBase>().curRoomPoint = new Vector2(y + 1, x);
            roomSpawn.mapDataArray[y + 1, x].GetComponent<RoomData>().playerIn = true;
            roomSpawn.mapDataArray[y + 1, x].GetComponent<RoomData>().RoomStart();

            StormCloudPosition.instance.transform.position = roomSpawn.mapDataArray[y + 1, x].transform.position - Vector3.up * 8;
        }
        else if (doorType == 2)
        {
            roomSpawn.mapDataArray[y, x - 1].transform.parent.gameObject.SetActive(true);

            player.position
                = roomSpawn.mapDataArray[y, x - 1].GetComponent<RoomData>().roomPos[doorType - 2].transform.position;
            player.GetComponent<PlayerBase>().curRoomPoint = new Vector2(y, x - 1);
            roomSpawn.mapDataArray[y, x - 1].GetComponent<RoomData>().playerIn = true;
            roomSpawn.mapDataArray[y, x - 1].GetComponent<RoomData>().RoomStart();

            StormCloudPosition.instance.transform.position = roomSpawn.mapDataArray[y, x - 1].transform.position - Vector3.up * 8;
        }
        else if (doorType == 3)
        {
            roomSpawn.mapDataArray[y - 1, x].transform.parent.gameObject.SetActive(true);

            player.position
                = roomSpawn.mapDataArray[y - 1, x].GetComponent<RoomData>().roomPos[doorType - 2].transform.position;
            player.GetComponent<PlayerBase>().curRoomPoint = new Vector2(y - 1, x);
            roomSpawn.mapDataArray[y - 1, x].GetComponent<RoomData>().playerIn = true;
            roomSpawn.mapDataArray[y - 1, x].GetComponent<RoomData>().RoomStart();

            StormCloudPosition.instance.transform.position = roomSpawn.mapDataArray[y - 1, x].transform.position - Vector3.up * 8;
        }

        playerIn = false;
    }
}
