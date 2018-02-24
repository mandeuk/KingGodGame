using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BlockSpawn : MonoBehaviour {
    public GameObject blockPos;
    public Block[,] blockArray;
    Block blockClones;

    // Use this for initialization
    void Awake () {
        blockArray = new Block[20,20];
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
            {
                blockClones = Instantiate(blockPos, new Vector3(1.2f * i, 1.2f * j, 0), blockPos.transform.rotation).GetComponent<Block>();
                blockArray[i, j] = blockClones;
            }
        
    }
	
	// Update is called once per frame
	void Update () {
        blockArray[10, 10].GetComponent<Block>().blockNum = 2;
    }
}
