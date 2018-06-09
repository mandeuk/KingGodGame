using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    // 이거 이름은 stageManager 인데 그냥 RoomManager로 씀.
    public static StageManager instance = null;
    public GameObject player;

    public int[,] mapSpawnArray;// = new int[9, 9] {   {0,0,0,0,0,0,0,0,0 },
                                                    //{0,0,0,0,0,3,0,0,0 },
                                                    //{0,0,2,0,0,1,0,0,0 },
                                                    //{0,4,1,1,1,1,1,0,0 },
                                                    //{0,0,5,1,1,0,1,0,0 },
                                                    //{0,0,0,0,1,0,0,0,0 },
                                                    //{0,0,0,0,0,0,0,0,0 },
                                                    //{0,0,0,0,0,0,0,0,0 },
                                                    //{0,0,0,0,0,0,0,0,0 }};
    

    // Use this for initialization
    void Awake () {
        instance = this;
        MapRandomSetting();
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

    void SetStartRoom()
    {
        //방 시작위치 설정하는 함수
        //방의 전체적인 생성모양이 예전에 잠깐 들었던 내용을 까먹어서 나중에 회의 한번 하고 수정할 예정
    }

    void SetBossRoom()
    {
        
    }

    void SearchRoute()
    {
        //A스타 비스무리한 원리를 활용하여 작성예정

        //메인루트 뚫기
        //1. 현재노드 -> 보스룸 거리측정(삼각함수)
        //2. 다음노드들 -> 보스룸 거리측정
        //3. 현재노드를 별도 저장 후 가장 가까운 노드로 이동
        //4. 2번부터 보스방이 나올 때 까지 다시 반복
        //※.여기서 보스방까지 바로 안가고 우회로를 뚫으면 보스방까지 조금 둘러서 갈 수 있을듯

        //그외 기타방 만들기
        //메인루트 뚫으며 저장해놓은 리스트를 거꾸로 돌아가며 잔가지처럼 뻗어나가게 만들 예정


        //노드 구조체를 만들어야할까?

        //시작노드 가져오기
        //

        
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
