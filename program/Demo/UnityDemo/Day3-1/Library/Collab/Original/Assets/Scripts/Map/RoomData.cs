using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomData : MonoBehaviour {
    public enum roomState : int
    {
        Wait = 1,
        Start = 2,
        Clear = 3
    }

    public bool isClear;
    public bool playerIn;
    public int x, y;

    public GameObject meshs;
    public GameObject fadeCamera;
    public GameObject[] roomPos = new GameObject[4];
    Transform player;
    GameObject cameraFade;

    public GameObject[] FluidFire = new GameObject[4];

    public int[,] mapSpawnArray = new int[9, 9];
    public List<GameObject> EnemyClones = new List<GameObject>();




    // Use this for initialization
    void Awake () {
        cameraFade = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player").transform;
        isClear = false;
        playerIn = false;
        //meshs.SetActive(false);
        
    }

    private void Start()
    {
        EnemyClones = transform.GetComponentInChildren<ObstacleData>().EnemyClones;
        mapSpawnArray = StageManager.instance.mapSpawnArray;
        RoomWait();
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
                RoomStart();
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
    }

    // 룸에 플레이어가 들어왔을때 시작하는 상태에서 호출
    public void RoomStart()
    {
        playerIn = true;
        if (transform.GetComponent<Animator>().enabled)
            transform.GetComponent<Animator>().SetBool("DoorOpen", false);
        for (int i = 0; i < EnemyClones.Count; i++) {
            EnemyClones[i].GetComponent<NavMeshAgent>().enabled = true;
            EnemyClones[i].GetComponent<EnemyMovement>().startMove();
        }
    }

    // 룸이 스폰되고 플레이어가 진입전의 상태에서 호출
    public void RoomWait()
    {
        if (transform.GetComponent<Animator>().enabled)
            transform.GetComponent<Animator>().SetBool("DoorOpen", true);
        for (int i = 0; i < EnemyClones.Count; i++)
        {
            EnemyClones[i].GetComponent<EnemyMovement>().stopMove();
            EnemyClones[i].GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    public IEnumerator RoomMove(int doorType)
    {
        player.GetComponent<PlayerMovement>().moveRoom = true;
        player.GetComponent<Animator>().SetTrigger("roomMove");
        StartCoroutine(cameraFade.transform.GetComponentInChildren<CameraFadeInOut>().FadeOut());

        cameraFade.transform.GetChild(0).gameObject.SetActive(false);
        cameraFade.transform.GetChild(1).gameObject.SetActive(false);
        cameraFade.transform.GetChild(2).gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);

        playerGoNextRoom(doorType);
        cameraFade.transform.position = player.transform.position;
        cameraFade.transform.GetChild(0).gameObject.SetActive(true);
        cameraFade.transform.GetChild(1).gameObject.SetActive(true);
        cameraFade.transform.GetChild(2).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(cameraFade.transform.GetComponentInChildren<CameraFadeInOut>().FadeIn());

        yield return new WaitForSeconds(0.8f);
        playerIn = false;
        player.GetComponent<PlayerMovement>().moveRoom = false;
        yield break;
    }

    void JudgeRoomDoorOpen()
    {
        if (mapSpawnArray[y, x + 1] > 0)
        {
            GetComponentInChildren<RoomColManager>().LeftDoorOpen();
        }
        if (mapSpawnArray[y, x - 1] > 0)
        {
            GetComponentInChildren<RoomColManager>().RightDoorOpen();
        }
        if (mapSpawnArray[y + 1, x] > 0)
        {
            GetComponentInChildren<RoomColManager>().FrontDoorOpen();
        }
        if (mapSpawnArray[y - 1, x] > 0)
        {
            GetComponentInChildren<RoomColManager>().BackDoorOpen();
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
            player.position 
                = RoomSpawn.mapDataArray[y, x + 1].GetComponent<RoomData>().roomPos[doorType + 2].transform.position;
            RoomSpawn.mapDataArray[y, x + 1].GetComponent<RoomData>().playerIn = true;
            playerIn = false;
        }
        else if (doorType == 1)
        {
            player.position
                = RoomSpawn.mapDataArray[y + 1, x].GetComponent<RoomData>().roomPos[doorType + 2].transform.position;
            RoomSpawn.mapDataArray[y + 1, x].GetComponent<RoomData>().playerIn = true;
            playerIn = false;
        }
        else if (doorType == 2)
        {
            player.position
                = RoomSpawn.mapDataArray[y, x - 1].GetComponent<RoomData>().roomPos[doorType - 2].transform.position;
            RoomSpawn.mapDataArray[y, x - 1].GetComponent<RoomData>().playerIn = true;
            playerIn = false;
        }
        else if (doorType == 3)
        {
            player.position
                = RoomSpawn.mapDataArray[y - 1, x].GetComponent<RoomData>().roomPos[doorType - 2].transform.position;
            RoomSpawn.mapDataArray[y - 1, x].GetComponent<RoomData>().playerIn = true;
            playerIn = false;
        }
    }
}
