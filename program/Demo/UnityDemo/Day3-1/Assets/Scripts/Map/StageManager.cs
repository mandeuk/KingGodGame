using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    // 이거 이름은 stageManager 인데 그냥 RoomManager로 씀.
    public static StageManager instance = null;
    public GameObject player;

    public int[,] mapSpawnArray = new int[9, 9] {   {0,0,0,0,0,0,0,0,0 },
                                                    {0,4,1,0,0,1,0,0,0 },
                                                    {0,0,1,1,1,1,0,0,0 },
                                                    {0,0,2,1,1,0,0,0,0 },
                                                    {0,0,1,0,1,0,0,0,0 },
                                                    {0,0,0,0,3,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 }};
    

    // Use this for initialization
    void Awake () {
        instance = this;
    }
}
