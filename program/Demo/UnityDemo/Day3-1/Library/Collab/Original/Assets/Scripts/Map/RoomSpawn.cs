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
                    GameObject roomsParentObj = new GameObject();

                    if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        roomsParentObj = spawnTwoDoorRoom();
                    }
                    else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
                    {
                        roomsParentObj = spawnRightDoorRoom();
                    }
                    else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        roomsParentObj = spawnLeftDoorRoom();
                    }
                    else
                    {
                        roomsParentObj = spawnNoDoorRoom();
                    }

                    // 여기다가 spawnObstacle를 판별하여 넣어야함.
                    // 일단 임시로 어짜피 장애물 바리에이션은 하나뿐이니까 위에다가 적음.
                    spawnObstacle(roomsParentObj.transform);
                    spawnRoomCol(roomsParentObj.transform);
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

    GameObject SpawnEmptyRoom()
    {
        GameObject roomsParentObj = Instantiate(Resources.Load("Prefabs/Map/EmptyRoom"),transform) as GameObject;

        return roomsParentObj;
    }

    GameObject spawnTwoDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/TwoDoorRoom"), transform) as GameObject;

        return roomClone;
    }

    GameObject spawnLeftDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/LeftDoorRoom"), transform) as GameObject;

        return roomClone;
    }

    GameObject spawnRightDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RightDoorRoom"), transform) as GameObject;

        return roomClone;
    }

    GameObject spawnNoDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/NoDoorRoom"), transform) as GameObject;

        return roomClone;
    }

    void spawnObstacle(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPathObstacles_1"), parent) as GameObject;
    }

    void spawnRoomCol(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RoomCol"), parent) as GameObject;
    }
}
