using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour {
    public static GameObject[,] mapDataArray = new GameObject[9, 9];

    public int[,] mapSpawnArray = new int[9, 9];

    // Use this for initialization
    void Awake() {
        mapSpawnArray = StageManager.instance.mapSpawnArray;
        SpawnRoom();
    }

    private void Start()
    {

    }


    public void SpawnRoom()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mapSpawnArray[i, j] > 0)
                {
                    GameObject roomsParentObj = new GameObject("RoomNumber" + i.ToString() + "_" + j.ToString());
                    roomsParentObj.transform.parent = transform;

                    spawnObstacle(roomsParentObj.transform).transform.position = new Vector3(i * 40, -1, j * 40);
                    spawnRoomCol(roomsParentObj.transform).transform.position = new Vector3(i * 40, 0, j * 40);

                    if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        roomsParentObj = spawnTwoDoorRoom(roomsParentObj.transform);
                    }
                    else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
                    {
                        roomsParentObj = spawnRightDoorRoom(roomsParentObj.transform);
                    }
                    else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        roomsParentObj = spawnLeftDoorRoom(roomsParentObj.transform);
                    }
                    else
                    {
                        roomsParentObj = spawnNoDoorRoom(roomsParentObj.transform);
                    }

                    // 여기다가 spawnObstacle를 판별하여 넣어야함.
                    // 일단 임시로 어짜피 장애물 바리에이션은 하나뿐이니까 위에다가 적음.

                    mapDataArray[i, j] = roomsParentObj;

                    mapDataArray[i, j].transform.position = new Vector3(i * 40, 0, j * 40);
                    mapDataArray[i, j].GetComponent<RoomData>().x = j;
                    mapDataArray[i, j].GetComponent<RoomData>().y = i;


                    if (mapSpawnArray[i, j] == 2) // 플레이어가 생성되야할 방.
                    {
                        GameObject.FindWithTag("Player").transform.position = new Vector3(i * 40, 0, j * 40);
                        mapDataArray[i, j].GetComponent<RoomData>().RoomStart();
                    }
                }
            }
        }
    }

    GameObject SpawnEmptyRoom(Transform parent)
    {
        GameObject roomsParentObj = Instantiate(Resources.Load("Prefabs/Map/EmptyRoom"), parent) as GameObject;

        return roomsParentObj;
    }

    GameObject spawnTwoDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/TwoDoorRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnLeftDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/LeftDoorRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnRightDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RightDoorRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnNoDoorRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/NoDoorRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnObstacle(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPathObstacles_1"), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnRoomCol(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RoomCol"), parent) as GameObject;

        return roomClone;
    }
}
