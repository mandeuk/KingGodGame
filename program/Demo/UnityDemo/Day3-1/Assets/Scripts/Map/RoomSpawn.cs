using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour {
    public static GameObject[,] mapDataArray = new GameObject[9, 9];
    GameObject cloudEffect;

    public int[,] mapSpawnArray = new int[9, 9];

    // Use this for initialization
    void Awake() {
        mapSpawnArray = StageManager.instance.mapSpawnArray;
        cloudEffect = Instantiate(Resources.Load("Prefabs/Effect/StormCloud"),transform) as GameObject;
        SpawnRoom();
    }

    public void SpawnRoom()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mapSpawnArray[i, j] > 0)
                {
                    int cnt = 0;
                    GameObject roomsParentObj = new GameObject("RoomNumber" + i.ToString() + "_" + j.ToString());
                    Vector3 roomPos = new Vector3(i * 70, 0, j * 70);
                    Quaternion obstacleObjAngle;
                    roomsParentObj.transform.parent = transform;

                    if (mapSpawnArray[i + 1, j] != 0)
                    {
                        cnt++;
                    }
                    if (mapSpawnArray[i, j + 1] != 0)
                    {
                        cnt++;
                    }
                    if (mapSpawnArray[i - 1, j] != 0)
                    {
                        cnt++;
                    }
                    if (mapSpawnArray[i, j - 1] != 0)
                    {
                        cnt++;
                    }

                    if (mapSpawnArray[i,j] == 2)    // 플레이어가 생성되는 방은 2
                    {
                        obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                        SpawnStartRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                    }


                    else if (mapSpawnArray[i, j] == 3)   // 보스 생성되는 방은 3
                    {
                        obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                        SpawnBossRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                    }

                    else if (mapSpawnArray[i, j] == 4)   // 상점방은 4
                    {
                        obstacleObjAngle = Quaternion.Euler(0, -90, 0);
                        SpawnItemRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                    }

                    else if (mapSpawnArray[i, j] == 5)   // lotus 드랍 방 5
                    {
                        obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                        SpawnLotusRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                    }

                    else
                    {
                        if (cnt == 1)   // 옆 방이 한개일때.
                        {
                            if (mapSpawnArray[i, j + 1] != 0)   // 왼쪽방이 뚫려있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, -90, 0);
                                spawnOnePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                            else if (mapSpawnArray[i + 1, j] != 0)   // 윗쪽방이 뚫려있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                                spawnOnePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                   roomPos + Vector3.down, obstacleObjAngle);
                            }
                            else if (mapSpawnArray[i, j - 1] != 0)   // 오른쪽방이 뚫려있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 90, 0);
                                spawnOnePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                            else if (mapSpawnArray[i - 1, j] != 0)   // 아랫쪽방이 뚫려있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 180, 0);
                                spawnOnePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                        }

                        else if (cnt == 2)
                        {
                            if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i - 1, j] == 0)
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 90, 0);
                                spawnTwoPath1Obstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }

                            if (mapSpawnArray[i, j + 1] == 0 && mapSpawnArray[i, j - 1] == 0)
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                                spawnTwoPath1Obstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }



                            if (mapSpawnArray[i, j + 1] == 0 && mapSpawnArray[i + 1, j] == 0)
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 180, 0);
                                spawnTwoPath2Obstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }

                            if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j - 1] == 0)
                            {
                                obstacleObjAngle = Quaternion.Euler(0, -90, 0);
                                spawnTwoPath2Obstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }

                            if (mapSpawnArray[i, j - 1] == 0 && mapSpawnArray[i - 1, j] == 0)
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                                spawnTwoPath2Obstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }

                            if (mapSpawnArray[i - 1, j] == 0 && mapSpawnArray[i, j + 1] == 0)
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 90, 0);
                                spawnTwoPath2Obstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                        }

                        else if (cnt == 3)
                        {
                            if (mapSpawnArray[i, j + 1] == 0)   // 왼쪽방이 닫혀있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 90, 0);
                                spawnThreePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                            else if (mapSpawnArray[i + 1, j] == 0)   // 윗쪽방이 닫혀있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 180, 0);
                                spawnThreePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                   roomPos + Vector3.down, obstacleObjAngle);
                            }
                            else if (mapSpawnArray[i, j - 1] == 0)   // 오른쪽방이 닫혀있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, -90, 0);
                                spawnThreePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                            else if (mapSpawnArray[i - 1, j] == 0)   // 아랫쪽방이 닫혀있을때
                            {
                                obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                                spawnThreePathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                                    roomPos + Vector3.down, obstacleObjAngle);
                            }
                        }

                        else
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            spawnAllPathObstacle(roomsParentObj.transform).transform.SetPositionAndRotation(
                               roomPos + Vector3.down, obstacleObjAngle);
                        }
                    }

                    spawnRoomCol(roomsParentObj.transform).transform.position = roomPos;

                    GameObject roomDataRoom;

                    if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        roomDataRoom = spawnTwoDoorRoom(roomsParentObj.transform);
                    }
                    else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
                    {
                        roomDataRoom = spawnRightDoorRoom(roomsParentObj.transform);
                    }
                    else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        roomDataRoom = spawnLeftDoorRoom(roomsParentObj.transform);
                    }
                    else
                    {
                        roomDataRoom = spawnNoDoorRoom(roomsParentObj.transform);
                    }

                    // 여기다가 spawnObstacle를 판별하여 넣어야함.
                    // 일단 임시로 어짜피 장애물 바리에이션은 하나뿐이니까 위에다가 적음.

                    
                    mapDataArray[i, j] = roomDataRoom;

                    mapDataArray[i, j].transform.position = roomPos;
                    mapDataArray[i, j].GetComponent<RoomData>().x = j;
                    mapDataArray[i, j].GetComponent<RoomData>().y = i;

                    roomsParentObj.SetActive(false);

                    if (mapSpawnArray[i,j] == 2)
                    {
                        roomsParentObj.SetActive(true);
                        GameObject.FindWithTag("Player").transform.position = roomPos - transform.forward * 2;
                        cloudEffect.transform.position = roomPos - Vector3.up * 8;
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

    GameObject spawnAllPathObstacle(Transform parent)
    {
        int i = 1;
        GameObject roomClone;
        while (Resources.Load("Prefabs/Map/Obstacles/AllPath_" + i.ToString()))
        {
            i++;
        }
        roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPath_" + Random.Range(1, i).ToString()), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnOnePathObstacle(Transform parent)
    {
        int i = 1;
        GameObject roomClone;
        while (Resources.Load("Prefabs/Map/Obstacles/OnePath_" + i.ToString()))
        {
            i++;
        }
        if (Random.Range(1, 3) == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPath_" + Random.Range(1, i).ToString()), parent) as GameObject;
        }
        else
        {
            roomClone = spawnAllPathObstacle(parent);
        }
        return roomClone;
    }

    GameObject spawnTwoPath1Obstacle(Transform parent)
    {
        int i = 1;
        GameObject roomClone;
        while (Resources.Load("Prefabs/Map/Obstacles/TwoPath1_" + i.ToString()))
        {
            i++;
        }
        if (Random.Range(1, 3) == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPath_" + Random.Range(1, i).ToString()), parent) as GameObject;
        }
        else
        {
            roomClone = spawnAllPathObstacle(parent);
        }
        return roomClone;
    }

    GameObject spawnTwoPath2Obstacle(Transform parent)
    {
        int i = 1;
        GameObject roomClone;
        while (Resources.Load("Prefabs/Map/Obstacles/TwoPath2_" + i.ToString()))
        {
            i++;
        }

        if (Random.Range(1, 3) == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPath_" + Random.Range(1, i).ToString()), parent) as GameObject;
        }
        else
        {
            roomClone = spawnAllPathObstacle(parent);
        }
        return roomClone;
    }

    GameObject spawnThreePathObstacle(Transform parent)
    {
        int i = 1;
        GameObject roomClone;
        while (Resources.Load("Prefabs/Map/Obstacles/ThreePath_" + i.ToString()))
        {
            i++;
        }
        if (Random.Range(1, 3) == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPath_" + Random.Range(1, i).ToString()), parent) as GameObject;
        }
        else
        {
            roomClone = spawnAllPathObstacle(parent);
        }
        return roomClone;
    }

    GameObject SpawnStartRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/StartRoom_1"), parent) as GameObject;

        return roomClone;
    }

    GameObject SpawnBossRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/BossRoom_1"), parent) as GameObject;

        return roomClone;
    }

    GameObject SpawnItemRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/ItemRoom_1"), parent) as GameObject;

        return roomClone;
    }

    GameObject SpawnLotusRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/AllPath_8"), parent) as GameObject;

        return roomClone;
    }


    GameObject spawnRoomCol(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RoomCol"), parent) as GameObject;

        return roomClone;
    }
}
