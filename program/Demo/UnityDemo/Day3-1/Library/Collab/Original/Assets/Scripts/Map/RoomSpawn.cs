using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour {

    public GameObject[,] mapDataArray = new GameObject[9, 9];

    public int[,] mapSpawnArray = new int[9, 9] {   {2,1,0,0,0,0,0,0,0 },
                                                    {1,1,1,0,0,0,0,0,0 },
                                                    {0,0,1,1,1,0,0,0,0 },
                                                    {0,0,1,0,0,0,0,0,0 },
                                                    {0,0,1,1,0,0,0,0,0 },
                                                    {0,0,0,3,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 }};

    // Use this for initialization
    void Awake() {
        SpawnRoom();
    }

    void InitRoom()
    {
        //for (int i = 0; i < 9; i++)
        //{
        //    for (int j = 0; j < 9; j++)
        //    {
        //        if(mapSpawnArray[i,j] > 0)
        //        {
        //            if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
        //            {
        //                mapDataArray[i, j] = spawnTwoDoorRoom();
        //            }
        //            else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
        //            {
        //                mapDataArray[i, j] = spawnRightDoorRoom();
        //            }
        //            else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
        //            {
        //                mapDataArray[i, j] = spawnLeftDoorRoom();
        //            }
        //            else
        //            {                 
        //                mapDataArray[i, j] = spawnNoDoorRoom();
        //            }
        //            mapDataArray[i, j].transform.position = new Vector3(i * 40, 0, j * 40);
        //        }
        //    }
        //}
    }

    public void SpawnRoom()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mapSpawnArray[i, j] > 0)
                {
                    GameObject roomsParentObj = SpawnEmptyRoom();

                    if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        spawnTwoDoorRoom(roomsParentObj.transform);
                        spawnObstacle(roomsParentObj.transform);
                        mapDataArray[i, j] = roomsParentObj;
                    }
                    else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
                    {
                        spawnRightDoorRoom(roomsParentObj.transform);
                        spawnObstacle(roomsParentObj.transform);
                        mapDataArray[i, j] = roomsParentObj;
                    }
                    else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        spawnLeftDoorRoom(roomsParentObj.transform);
                        spawnObstacle(roomsParentObj.transform);
                        mapDataArray[i, j] = roomsParentObj;
                    }
                    else
                    {
                        spawnNoDoorRoom(roomsParentObj.transform);
                        spawnObstacle(roomsParentObj.transform);
                        mapDataArray[i, j] = roomsParentObj;
                    }
                    mapDataArray[i, j].transform.position = new Vector3(i * 40, 0, j * 40);
                }
            }
        }
    }

    GameObject SpawnEmptyRoom()
    {
        GameObject roomsParentObj = Instantiate(Resources.Load("Prefabs/Map/EmptyRoom"),transform) as GameObject;

        return roomsParentObj;
    }

    void spawnTwoDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/TwoDoorRoom"), parent) as GameObject;
    }

    void spawnLeftDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/LeftDoorRoom"), parent) as GameObject;
    }

    void spawnRightDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RightDoorRoom"), parent) as GameObject;
    }

    void spawnNoDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/TwoDoorRoom"), parent) as GameObject;
    }

    void spawnObstacle(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/ObstaclesTypeAllPath_1"), parent) as GameObject;
    }

    void judgeRoomArray(int i , int j)
    {
        if (mapSpawnArray[i, j] > 0)
        {
            GameObject roomsParentObj = SpawnEmptyRoom();

            if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
            {
                spawnTwoDoorRoom(roomsParentObj.transform);
                spawnObstacle(roomsParentObj.transform);
                mapDataArray[i, j] = roomsParentObj;
            }
            else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
            {
                spawnRightDoorRoom(roomsParentObj.transform);
                spawnObstacle(roomsParentObj.transform);
                mapDataArray[i, j] = roomsParentObj;
            }
            else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
            {
                spawnLeftDoorRoom(roomsParentObj.transform);
                spawnObstacle(roomsParentObj.transform);
                mapDataArray[i, j] = roomsParentObj;
            }
            else
            {
                spawnNoDoorRoom(roomsParentObj.transform);
                spawnObstacle(roomsParentObj.transform);
                mapDataArray[i, j] = roomsParentObj;
            }
            mapDataArray[i, j].transform.position = new Vector3(i * 40, 0, j * 40);
        }
    }
}
