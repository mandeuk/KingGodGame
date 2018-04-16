using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    // 이거 이름은 stageManager 인데 그냥 RoomManager로 씀.
    public static StageManager instance = null;
    public GameObject player;

    public int[,] mapSpawnArray = new int[9, 9] {   {0,0,0,0,0,0,0,0,0 },
                                                    {0,4,1,0,0,1,0,0,0 },
                                                    {0,0,1,2,1,1,0,0,0 },
                                                    {0,0,1,1,1,0,0,0,0 },
                                                    {0,0,1,0,1,0,0,0,0 },
                                                    {0,0,0,0,3,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 }};
    

    // Use this for initialization
    void Awake () {

        if (instance)//인스턴스가 생성되어있는가?
        {
            DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }

        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mapSpawnArray[i, j] == 2) // 플레이어가 생성되야할 방.
                {
                    player.transform.position = new Vector3(i * 40, 0, j * 40);
                    RoomSpawn.mapDataArray[i, j].GetComponent<RoomData>().playerIn = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
