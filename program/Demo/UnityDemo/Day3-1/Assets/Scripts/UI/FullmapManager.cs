using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullmapManager : MonoBehaviour {
    public static FullmapManager instance = null;

    GameObject[,] fullmap = new GameObject[9, 9];
    Text atkPowerText, atkSpeedText, moveSpeedText;
    int[,] mapArray = new int[9, 9];
    int myposX, myposY;

    Sprite contigfindroom, bossroom, findroom, rocationroom;

    void Awake()
    {
        if (instance)//인스턴스가 생성되어있는가?
        {
            //DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            //DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }

        contigfindroom = (Sprite)Resources.Load("Image/UI/minimap/contigFindRoom", typeof(Sprite));
        bossroom = (Sprite)Resources.Load("Image/UI/minimap/bossRoom", typeof(Sprite));
        findroom = (Sprite)Resources.Load("Image/UI/minimap/findRoom", typeof(Sprite));
        rocationroom = (Sprite)Resources.Load("Image/UI/minimap/rocationRoom", typeof(Sprite));

        atkPowerText = this.transform.GetChild(83).gameObject.GetComponent<Text>();
        atkSpeedText = this.transform.GetChild(84).gameObject.GetComponent<Text>();
        moveSpeedText = this.transform.GetChild(85).gameObject.GetComponent<Text>();
    }

    public void UpdateFullmap(GameObject mm)//mm에는 미니맵의 큰 배경이미지를 첨부해주세요.
    {
        GetFullmapObject(mm);//하이어라키에 있는 미니맵속 작은방이미지의 게임오브젝트 불러오기
        GetMapState();//맵 전체정보 가져오기

        //캐릭터 위치 가져오기

        //미니맵 세팅하기
        SetFullmapObject();
    }

    void GetFullmapObject(GameObject mm)
    {
        int hiarachylist = 2;
        for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
            {
                fullmap[x, y] = mm.transform.GetChild(hiarachylist).gameObject;
                hiarachylist++;

                fullmap[x, y].SetActive(false);
            }
    }

    void GetMapState()
    {
        mapArray = StageManager.instance.mapSpawnArray;
    }

    void SetFullmapObject()
    {
        myposX = (int)PlayerBase.instance.curRoomPoint.x;
        myposY = (int)PlayerBase.instance.curRoomPoint.y;

        for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
            {
                if (mapArray[x, y] != 0)
                {
                    fullmap[x, y].SetActive(true);

                    Image roomicon;
                    roomicon = fullmap[x, y].GetComponent<Image>();

                    if (x == myposX && y == myposY)
                    {
                        roomicon.sprite = rocationroom;
                    }
                    else
                    {
                        switch (mapArray[x, y])
                        {
                            case 2://시작방
                                roomicon.sprite = findroom;
                                break;
                            //case 1://일반방들
                            //case 3:
                            //case 4:
                            //case 5:
                            //case 6:
                            //case 10:
                            //case 11:
                            //    break;
                            case 7://보스방
                            case 8:
                            case 9:
                                roomicon.sprite = bossroom;
                                if ((RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isClear == false) &&(RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false))
                                {
                                    fullmap[x, y].SetActive(false);
                                }
                                break;
                            default:
                                {
                                    if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isClear)
                                    {
                                        roomicon.sprite = findroom;
                                    }
                                    else if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind)
                                    { }
                                    else
                                    {
                                        roomicon.sprite = contigfindroom;
                                        fullmap[x, y].SetActive(false);
                                    }
                                }
                                break;
                        }
                    }

                    if (myposX == x && myposY + 1 == y) { fullmap[x, y].SetActive(true); }
                    else if (myposX == x && myposY - 1 == y) { fullmap[x, y].SetActive(true); }
                    else if (myposX + 1 == x && myposY == y) { fullmap[x, y].SetActive(true); }
                    else if (myposX - 1 == x && myposY == y) { fullmap[x, y].SetActive(true); }
                    

                }
            }
    }

    public void UpdateStatusText()
    {
        atkPowerText.text = ("Damage : " + PlayerBase.instance.attackPower.ToString());
        atkSpeedText.text = ("Attack Speed : " + PlayerBase.instance.attackSpeed.ToString());
        moveSpeedText.text = ("Move Speed : " + PlayerBase.instance.moveSpeed.ToString());
    }
}
