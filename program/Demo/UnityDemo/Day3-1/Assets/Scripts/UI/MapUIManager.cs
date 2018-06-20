using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateMinimap()
    {
        int playerPosX=0, playerPosY=0;
        int minX, minY, maxX, maxY;

        

        minX = playerPosX-2;
        maxX = playerPosX+2;
        minY = playerPosY-2;
        maxY = playerPosY+2;
        for (int y = minY; y < maxY; ++y)
        {
            for(int x = minX; x < maxX; ++x)
            {
                switch(StageManager.instance.mapSpawnArray[y, x])
                {
                    case 1:
                        break;
                }
            }
        }
        //
    }
}
