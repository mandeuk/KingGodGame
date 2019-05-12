using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour {
    public static RoomSpawn instance;
    public GameObject[,] mapDataArray = new GameObject[9, 9];
    GameObject cloudEffect;

    public int[,] mapSpawnArray = new int[9, 9];

    // Use this for initialization
    void Awake() {
        instance = this;
        mapSpawnArray = StageManager.instance.mapSpawnArray;
        SpawnRoom();
    }

    public void SpawnRoom()
    {
        if (!GameManager.instance.testMod)  // 테스트모드가 아닐경우 원래있던 맵스폰어레이를 읽음.
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int cnt = 0;
                    GameObject roomsParentObj = new GameObject("RoomNumber" + i.ToString() + "_" + j.ToString());
                    Vector3 roomPos = new Vector3(i * 70, 0, j * 70);
                    Quaternion obstacleObjAngle;
                    roomsParentObj.transform.parent = transform;

                    if (mapSpawnArray[i, j] > 0)
                    {
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

                        if (mapSpawnArray[i, j] == 2)    // 플레이어가 생성되는 방은 2
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnStartRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        else if (mapSpawnArray[i, j] == 3)   // 보스 생성되는 방은 3 (보스없이 몹많은방)
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnBossRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        else if (mapSpawnArray[i, j] == 4)   // 상점방, 임프방은 4
                        {
                            obstacleObjAngle = Quaternion.Euler(0, -90, 0);
                            SpawnItemRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        else if (mapSpawnArray[i, j] == 5)   // lotus 드랍 및 레이스보스도 되는 방 5
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnStage1BossRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        //else if (mapSpawnArray[i, j] == 6)   // 작은검 드랍 방 6
                        //{
                        //    obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                        //    SpawnLotusRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                        //                roomPos + Vector3.down, obstacleObjAngle);
                        //}

                        else if (mapSpawnArray[i, j] == 7)   // 레이스보스방 7
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnStage1BossRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        else if (mapSpawnArray[i, j] == 8)   // 콜로서스 방 8
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnCorosusRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        else if (mapSpawnArray[i, j] == 9)   // 하이프리스트 방 9
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnHighPriestRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
                                        roomPos + Vector3.down, obstacleObjAngle);
                        }

                        else if (mapSpawnArray[i, j] == 10)   // 그냥 stage2 의 일자방.
                        {
                            obstacleObjAngle = Quaternion.Euler(0, 0, 0);
                            SpawnStage2TestroomSpawn(roomsParentObj.transform).transform.SetPositionAndRotation(
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
                                obstacleObjAngle = Quaternion.Euler(0, 90 * Random.Range(1, 5), 0);
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
                        mapDataArray[i, j].GetComponent<RoomData>().roomNumber = mapSpawnArray[i, j];

                        roomsParentObj.SetActive(false);

                        if (mapSpawnArray[i, j] == 2)
                        {
                            if (GameManager.playerPosition.stageNum == 1)
                            {
                                cloudEffect = Instantiate(Resources.Load("Prefabs/Effect/StormCloud"), transform) as GameObject;
                            }
                            else if (GameManager.playerPosition.stageNum == 2)
                            {
                                cloudEffect = Instantiate(Resources.Load("Prefabs/Effect/StormCloudStage2"), transform) as GameObject;
                            }
                            roomsParentObj.SetActive(true);
                            PlayerBase.instance.transform.position = roomPos;
                            PlayerBase.instance.curRoomPoint = new Vector2(j, i);
                            cloudEffect.transform.position = roomPos - Vector3.up * 6;
                            mapDataArray[i, j].GetComponent<RoomData>().RoomStart();
                        }
                    }
                }
            }
        }

        else
        {
            GameObject roomsParentObj = new GameObject();
            Vector3 roomPos = new Vector3(0, 0);
            Quaternion obstacleObjAngle;
            roomsParentObj.transform.parent = transform;


            obstacleObjAngle = Quaternion.Euler(0, 90 * Random.Range(1, 5), 0);
            //spawnEmptyRoom(roomsParentObj.transform).transform.SetPositionAndRotation(
            //   roomPos + Vector3.down, obstacleObjAngle);

            spawnRoomCol(roomsParentObj.transform).transform.position = roomPos;

            GameObject EmptyRoom;

            EmptyRoom = spawnTwoDoorRoom(roomsParentObj.transform);

            mapDataArray[0, 0] = EmptyRoom;

            mapDataArray[0, 0].transform.position = roomPos;
            mapDataArray[0, 0].GetComponent<RoomData>().x = 0;
            mapDataArray[0, 0].GetComponent<RoomData>().y = 0;
            mapDataArray[0, 0].GetComponent<RoomData>().roomNumber = mapSpawnArray[0, 0];
        }
    }

    GameObject spawnTwoDoorRoom(Transform parent)
    {
        GameObject roomClone = new GameObject();
        if (GameManager.playerPosition.stageNum == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage1Wall/TwoDoorRoomFix"), parent) as GameObject;
        }
        else if (GameManager.playerPosition.stageNum == 2)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage2Wall/TwoDoorRoomFrost"), parent) as GameObject;
        }

        return roomClone;
    }

    GameObject spawnLeftDoorRoom(Transform parent)
    {
        GameObject roomClone = new GameObject();
        if (GameManager.playerPosition.stageNum == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage1Wall/LeftDoorRoomFix"), parent) as GameObject;
        }
        else if (GameManager.playerPosition.stageNum == 2)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage2Wall/LeftDoorRoomFrost"), parent) as GameObject;
        }

        return roomClone;
    }

    GameObject spawnRightDoorRoom(Transform parent)
    {
        GameObject roomClone = new GameObject();
        if (GameManager.playerPosition.stageNum == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage1Wall/RightDoorRoomFix"), parent) as GameObject;
        }
        else if (GameManager.playerPosition.stageNum == 2)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage2Wall/RightDoorRoomFrost"), parent) as GameObject;
        }

        return roomClone;
    }

    GameObject spawnNoDoorRoom(Transform parent)
    {
        GameObject roomClone = new GameObject();
        if (GameManager.playerPosition.stageNum == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage1Wall/NoDoorRoomFix"), parent) as GameObject;
        }
        else if (GameManager.playerPosition.stageNum == 2)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Stage2Wall/NoDoorRoomFrost"), parent) as GameObject;
        }

        return roomClone;
    }

    GameObject spawnEmptyRoom(Transform parent)
    {
        GameObject roomClone;
        roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/EmptyRoom"), parent) as GameObject;

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
        if (!(Random.Range(1, 4) == 1))
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/OnePath_" + Random.Range(1, i).ToString()), parent) as GameObject;
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

        if (!(Random.Range(1, 4) == 1))
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/TwoPath1_" + Random.Range(1, i).ToString()), parent) as GameObject;
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

        if (!(Random.Range(1, 4) == 1))
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/TwoPath2_" + Random.Range(1, i).ToString()), parent) as GameObject;
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
        //if (GameManager.playerPosition.stageNum == 1)
        //{
            while (Resources.Load("Prefabs/Map/Obstacles/ThreePath_" + i.ToString()))
            {
                i++;
            }
            if (!(Random.Range(1, 4) == 1))
            {
                roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/ThreePath_" + Random.Range(1, i).ToString()), parent) as GameObject;
            }
            else
            {
                roomClone = spawnAllPathObstacle(parent);
            }
        //}

        //else if (GameManager.playerPosition.stageNum == 2)
        //{

        //}
        return roomClone;
    }

    GameObject SpawnStartRoom(Transform parent)
    {
        GameObject roomClone = new GameObject();
        if (GameManager.playerPosition.stageNum == 1)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/StartRoom_1"), parent) as GameObject;
        }
        else if(GameManager.playerPosition.stageNum == 2)
        {
            roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/Stage2StartRoom_1"), parent) as GameObject;

        }
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

    GameObject SpawnStage1BossRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/Stage1BossRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject SpawnCorosusRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/Stage2CorosusRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject SpawnHighPriestRoom(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/HighPriestRoom"), parent) as GameObject;

        return roomClone;
    }

    GameObject SpawnStage2TestroomSpawn(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/Obstacles/Stage2TwoPath1_1"), parent) as GameObject;

        return roomClone;
    }

    GameObject spawnRoomCol(Transform parent)
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RoomCol"), parent) as GameObject;

        return roomClone;
    }


}
