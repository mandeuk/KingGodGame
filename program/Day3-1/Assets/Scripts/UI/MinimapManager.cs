/*
 * 날짜 20180731
 * 최초 작성자 이인호
 * 목적 미니맵의 UI 이미지 오브젝트, 맵의 상태값을 가져와 올바르게 미니맵을 표시할 수 있게 해주는 미니맵 매니저를 만들기 위함
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour {
    public static MinimapManager instance = null;

    GameObject[,] minimap = new GameObject[5, 5];
    GameObject minimapObject;
    int[,] mapArray = new int[9, 9];
    int myposX = 0;
    int myposY = 0;

    Sprite contigfindroom, bossroom, findroom, rocationroom;


    void Awake () {
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


        //myposX = 2;
        //myposY = 2;

        contigfindroom = (Sprite)Resources.Load("Image/UI/minimap/contigFindRoom", typeof(Sprite));
        bossroom = (Sprite)Resources.Load("Image/UI/minimap/bossRoom", typeof(Sprite));
        findroom = (Sprite)Resources.Load("Image/UI/minimap/findRoom", typeof(Sprite));
        rocationroom = (Sprite)Resources.Load("Image/UI/minimap/rocationRoom", typeof(Sprite));

    }
	

    public void UpdateMinimap(GameObject mm)//mm에는 미니맵의 큰 배경이미지를 첨부해주세요.
    {
        //캐릭터 위치 가져오기
        myposX = (int)PlayerBase.instance.curRoomPoint.x;
        myposY = (int)PlayerBase.instance.curRoomPoint.y;

        GetMinimapObject(mm);//하이어라키에 있는 미니맵속 작은방이미지의 게임오브젝트 불러오기
        GetMapState();//맵 전체정보 가져오기
        
        //미니맵 세팅하기
        SetMinimapObject();
    }

    void GetMinimapObject(GameObject mm)
    {
        int hiarachylist = 1;
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 5; y++)
            {
                minimap[x, y] = mm.transform.GetChild(hiarachylist).gameObject;

                minimap[x, y].transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

                hiarachylist++;

                minimap[x, y].SetActive(false);
            }
    }

    void GetMapState()
    {
        mapArray = StageManager.instance.mapSpawnArray;
    }

    void SetMinimapObject()
    {
        for (int x = (myposX-2); x < (myposX+3); x++)//x, y는 전체맵 좌표
            for (int y = (myposY-2); y < (myposY+3); y++)
            {
                int minix, miniy;//mini x, y는 미니맵 속에 있는 5x5 크기의 배열좌표
                minix = (x - (myposX - 2));
                miniy = (y - (myposY - 2));

                if (minix < 0)
                    minix = 0;
                if (minix > 4)
                    minix = 4;
                if (miniy < 0)
                    miniy = 0;
                if (miniy > 4)
                    miniy = 4;
                
                //탐색한 방의 isFind를 true로 설정하여 ?방이더라도 발견한 방이면 거리가 멀어져도 미니맵에 표시함
                if ((myposX == x) && (myposY == y))
                {
                    if (RoomSpawn.instance.mapDataArray[x, y] != null)
                        if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false)
                            RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind = true;
                }
                if ((myposX - 1 == x) && (myposY == y))
                {
                    if (RoomSpawn.instance.mapDataArray[x, y] != null)
                        if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false)
                            RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind = true;
                }
                if ((myposX + 1 == x) && (myposY == y))
                {
                    if (RoomSpawn.instance.mapDataArray[x, y] != null)
                        if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false)
                            RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind = true;
                }
                if ((myposX == x) && (myposY - 1 == y))
                {
                    if (RoomSpawn.instance.mapDataArray[x, y] != null)
                        if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false)
                            RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind = true;
                }
                if ((myposX == x) && (myposY + 1 == y))
                {
                    if (RoomSpawn.instance.mapDataArray[x, y] != null)
                        if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false)
                            RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind = true;
                }

                if ((x < 0) || (x >= 9)) { }

                else if ((y < 0) || (y >= 9)) { }//break;을 넣으면 내 캐릭터가 x,y좌표 0~1되는 방에 들어갔을 때 다른 방의 아이콘이 안켜짐

                else if (mapArray[x, y] != 0)
                {
                    minimap[minix, miniy].SetActive(true);

                    switch (mapArray[x, y])
                    {
                        case 1:
                            minimap[minix, miniy].SetActive(true);
                            break;
                        case 2://2번 : 시작방
                            {
                                minimap[minix, miniy].SetActive(true);
                                minimap[minix, miniy].transform.GetChild(0).GetComponent<Image>().sprite = findroom;
                                break;
                            }
                        //case 3://3번 : 몬스터 짱많은방
                        //case 4://4번 : 투표 및 임프방
                        //case 5://5번 : 로투스 방
                        //case 6://6번 : 스몰소드방
                        //case 10:
                        //case 11:
                        //    
                        //    break;
                        case 7://7번 : 보스방
                        case 8://8번 : 코로서스방
                        case 9:
                            {
                                Image roomicon;
                                minimap[minix, miniy].SetActive(true);
                                roomicon = minimap[minix, miniy].transform.GetChild(0).GetComponent<Image>();
                                roomicon.sprite = bossroom;

                            }
                            break;
                        default://일반방
                            {
                                minimap[minix, miniy].SetActive(true);
                                if (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isClear)
                                    minimap[minix, miniy].transform.GetChild(0).GetComponent<Image>().sprite = findroom;
                                else
                                    minimap[minix, miniy].transform.GetChild(0).GetComponent<Image>().sprite = contigfindroom;


                            }
                            break;
                    }//end of switch 방마다 이미지설정 및 SetActive설정 해주기

                    //찾은방만 멀리까지 보이고 찾지않은방은 종류상관없이 모두 안보이게
                    if ((RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isClear == false) && (RoomSpawn.instance.mapDataArray[x, y].GetComponent<RoomData>().isFind == false))
                    {
                        if (minix == 0 && (miniy >= 0 && miniy <= 4))
                        {
                            minimap[minix, miniy].SetActive(false);
                        }
                        else if (minix == 4 && (miniy >= 0 && miniy <= 4))
                        {
                            minimap[minix, miniy].SetActive(false);
                        }
                        if (miniy == 0 && (minix >= 0 && minix <= 4))
                        {
                            minimap[minix, miniy].SetActive(false);
                        }
                        else if (miniy == 4 && (minix >= 0 && minix <= 4))
                        {
                            minimap[minix, miniy].SetActive(false);
                        }
                        if (((minix == 1 && miniy == 1) || (minix == 1 && miniy == 3)) || ((minix == 3 && miniy == 1) || (minix == 3 && miniy == 3)))
                        {
                            minimap[minix, miniy].SetActive(false);
                        }
                    }

                }//end of if 방이 존재할때


                else if (mapArray[x, y] == 0)
                {
                    minimap[minix, miniy].SetActive(false);
                }//end of if 방이 없을때

                

            }//end of for
    }

    void SetFindedMap()
    {
        
    }

}//end of public class MinimapManager
