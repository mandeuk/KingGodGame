using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public static StageManager instance = null;
    

    public int[,] mapSpawnArray = new int[9, 9] {   {2,1,1,0,0,0,0,0,0 },
                                                    {1,1,1,0,0,0,0,0,0 },
                                                    {0,0,1,1,0,0,0,0,0 },
                                                    {0,0,1,0,0,0,0,0,0 },
                                                    {0,0,1,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 },
                                                    {0,0,0,0,0,0,0,0,0 }};

    int[,] test1;

    // Use this for initialization
    void Awake () {
        test1 = mapSpawnArray;
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
