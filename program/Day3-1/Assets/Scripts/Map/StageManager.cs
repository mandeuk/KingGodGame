using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Node
{
    public int x, y;
    public float dist;
}

public class StageManager : MonoBehaviour {
    // 이거 이름은 stageManager 인데 그냥 RoomManager로 씀.
    public static StageManager instance = null;
    public int stageNum;
    public GameObject player;

    public int[,] mapSpawnArray = new int[9, 9] {   {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,2,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 }};


    // Use this for initialization
    void Awake() {
        instance = this;
        stageNum = 1;
        TestRoomArraySetting();
        //MapRandomSetting();
        //SearchAndSetRoom();
        StartCoroutine(CameraSetting());
    }

    void TestRoomArraySetting()
    {
        // 2번 : 시작방
        // 3번 : 몬스터 짱많은방
        // 4번 : 투표 및 임프방
        // 5번 : 로투스 방
        // 6번 : 스몰소드방
        // 7번 : 보스방
        // 8번 : 코로서스방
        // 9번 : 하이프리스트방
        // 10번 : stage2 시연하기위한 임시 방
        // 11번
        if (GameManager.playerPosition.stageNum == 1)
        {
            mapSpawnArray = new int[9, 9] { {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,2,0,0,0,0,0,0 },
                                            {0,4,6,0,0,0,0,0,0 },
                                            {0,0,5,3,9,0,0,0,0 },
                                            {0,0,11,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 }};
            SoundManager.instance.playStage1Music();
        }
        else if (GameManager.playerPosition.stageNum == 2)
        {
            mapSpawnArray = new int[9, 9] { {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,2,0,0,0,0,0,0 },
                                            {0,0,10,0,0,0,0,0,0 },
                                            {0,0,8,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 },
                                            {0,0,0,0,0,0,0,0,0 }};
            SoundManager.instance.playStage2Music();
        }
    }

    void MapRandomSetting()
    {
        mapSpawnArray = new int[9, 9] {   {0,0,0,0,0,0,0,0,0 },
                                          {0,0,0,0,0,3,0,0,0 },
                                          {0,0,2,0,0,1,0,0,0 },
                                          {0,4,1,1,1,1,1,0,0 },
                                          {0,0,5,1,1,0,1,0,0 },
                                          {0,0,0,0,1,0,0,0,0 },
                                          {0,0,0,0,0,0,0,0,0 },
                                          {0,0,0,0,0,0,0,0,0 },
                                          {0,0,0,0,0,0,0,0,0 }};
    }

    IEnumerator CameraSetting()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        yield return new WaitForSeconds(1.0f);
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        yield break;
    }

    Node SetStartRoom(Node startroom)
    {
        //방 시작위치 설정하는 함수
        //방의 전체적인 생성모양이 예전에 잠깐 들었던 내용을 까먹어서 나중에 회의 한번 하고 수정할 예정
        startroom.x = 1;
        startroom.y = 1;

        mapSpawnArray[startroom.x, startroom.y] = 2;
        return startroom;
    }

    Node SetBossRoom(Node bossroom)
    {
        bossroom.x = 5;
        bossroom.y = 5;

        mapSpawnArray[bossroom.x, bossroom.y] = 3;
        return bossroom;
    }

    void SearchAndSetRoom()
    {
        Node startroom = new Node();
        Node bossroom = new Node();
        Node curnode, leftnode, rightnode;
        startroom = SetStartRoom(startroom);
        startroom.dist = 0;
        bossroom = SetBossRoom(bossroom);
        bossroom.dist = 0;


        startroom.dist = MySqrt(startroom.x, startroom.y, bossroom.x, bossroom.y);
        curnode = startroom;

        while (true)
        {
            if (mapSpawnArray[curnode.y, curnode.x] != 0)
            {
                leftnode.x = curnode.x;
                leftnode.y = curnode.y + 1;
                leftnode.dist = MySqrt(leftnode.x, leftnode.y, bossroom.x, bossroom.y);

                rightnode.x = curnode.x + 1;
                rightnode.y = curnode.y;
                rightnode.dist = MySqrt(rightnode.x, rightnode.y, bossroom.x, bossroom.y);

                if (leftnode.dist == rightnode.dist)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            curnode = leftnode;
                            break;
                        case 2:
                            curnode = rightnode;
                            break;
                    }

                }
                else if (leftnode.dist < rightnode.dist)
                {
                    curnode = leftnode;
                }
                else if (leftnode.dist > rightnode.dist)
                {
                    curnode = rightnode;
                }
            }
            else if (mapSpawnArray[curnode.y, curnode.x] == 0)
            {
                mapSpawnArray[curnode.y, curnode.x] = 1;
            }

            if (mapSpawnArray[curnode.y, curnode.x] == mapSpawnArray[bossroom.y, bossroom.x])
            {
                break;
            }
        }

        //A스타 비스무리한 원리를 활용하여 작성예정

        //메인루트 뚫기
        //1. 현재노드 -> 보스룸 거리측정(삼각함수)
        //2. 다음노드들 -> 보스룸 거리측정
        //3. 현재노드를 별도 저장 후 가장 가까운 노드로 이동
        //4. 2번부터 보스방이 나올 때 까지 다시 반복
        //※.여기서 보스방까지 바로 안가고 우회로를 뚫으면 보스방까지 조금 둘러서 갈 수 있을듯

        //그외 기타방 만들기
        //메인루트 뚫으며 저장해놓은 리스트를 거꾸로 돌아가며 잔가지처럼 뻗어나가게 만들 예정


        ///노드 구조체를 만들어야할까?
        //시작노드 찍기    SetStartRoom();
        //보스방노드 찍기  SetBossRoom();

        //시작노드 가져오기
        //시작노드 기준 위->오른쪽->아래->왼쪽 순으로 보스방간의 거리 측정 후 거리정보 저장 MySqrt
        //가장 가까운 노드로 이동, 이 때 거리가 같은 노드가 있다면 rand함수로 랜덤이동
        //이동한 노드의 방 상태값을 1로 변경
        //위 동작을 반복하되 거리값이 0인 노드만 거리를 측정하고 새로 측정한 노드사이에서 다음으로 이동할 노드 선택
    }

    float MySqrt(int x1, int y1, int x2, int y2)//노드 거리계산용
    {
        float myresult = 0.0f;
        float tempx, tempy;
        tempx = (float)(x1 - x2);
        tempy = (float)(y1 - y2);

        if (tempx < 0.0f)
            tempx = -tempx;
        if (tempy < 0.0f)
            tempy = -tempy;

        tempx = tempx * tempx;
        tempy = tempy * tempy;

        myresult = tempx + tempy;

        return myresult;
    }
}
