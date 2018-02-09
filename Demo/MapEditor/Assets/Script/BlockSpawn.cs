using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour {
    public GameObject blockPos;
    Block blockClones;

    // Use this for initialization
    void Start () {
        List<Block> blockList = new List<Block>();

        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
            {
                blockClones = Instantiate(blockPos, new Vector3(1.2f * i, 1.2f * j, 0), blockPos.transform.rotation).GetComponent<Block>();
                blockList.Add(blockClones);
            }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
