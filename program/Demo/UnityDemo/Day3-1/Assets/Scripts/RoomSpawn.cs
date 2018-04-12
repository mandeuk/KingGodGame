using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour {

    public GameObject[,] mapDataArray = new GameObject[9, 9];
    public GameObject roomsParentObj;

    public int[,] mapSpawnArray = new int[9, 9] {   {2,1,1,0,0,0,0,0,0 },
                                                    {1,1,1,0,0,0,0,0,0 },
                                                    {0,0,1,1,0,0,0,0,0 },
                                                    {0,0,1,0,0,0,0,0,0 },
                                                    {0,0,1,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 }};

    // Use this for initialization
    void Awake() {
        InitRoom();
    }
    // Update is called once per frame

    void InitRoom()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if(mapSpawnArray[i,j] > 0)
                {
                    if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        mapDataArray[i, j] = spawnTwoDoorRoom();
                    }
                    else if (mapSpawnArray[i + 1, j] != 0 && mapSpawnArray[i, j + 1] == 0)
                    {
                        mapDataArray[i, j] = spawnRightDoorRoom();
                    }
                    else if (mapSpawnArray[i + 1, j] == 0 && mapSpawnArray[i, j + 1] != 0)
                    {
                        mapDataArray[i, j] = spawnLeftDoorRoom();
                    }
                    else
                    {                 
                        mapDataArray[i, j] = spawnNoDoorRoom();
                    }
                    mapDataArray[i, j].transform.position = new Vector3(i * 40, 0, j * 40);
                }
            }
        }
    }

    GameObject spawnTwoDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/TwoDoorRoom"), roomsParentObj.transform) as GameObject;

        return roomClone;
    }

    GameObject spawnLeftDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/LeftDoorRoom"), roomsParentObj.transform) as GameObject;

        return roomClone;
    }

    GameObject spawnRightDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/RightDoorRoom"), roomsParentObj.transform) as GameObject;
        
        return roomClone;
    }

    GameObject spawnNoDoorRoom()
    {
        GameObject roomClone = Instantiate(Resources.Load("Prefabs/Map/TwoDoorRoom"), roomsParentObj.transform) as GameObject;
        
        return roomClone;
    }
}
