using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




public class Block : MonoBehaviour {
    public bool selectBlockClone;
    public int blockNum;

    [Serializable]
    public class Blocks
    {
        int blockType;
    }
    

    // Use this for initialization
    void Awake() {
        blockNum = 1;
        selectBlockClone = false;
    }
	
	// Update is called once per frame
	void Update () {
        SelectObjectBlock();
    }

    public void SelectObjectBlock()
    {
        if (blockNum == 1)
            transform.GetComponent<MeshRenderer>().material.color = Color.white;

        if (blockNum == 2)
            transform.GetComponent<MeshRenderer>().material.color = Color.red;

        if (blockNum == 3)
            transform.GetComponent<MeshRenderer>().material.color = Color.blue;

        if (blockNum == 4)
            transform.GetComponent<MeshRenderer>().material.color = Color.black;

        if (blockNum == 5)
            transform.GetComponent<MeshRenderer>().material.color = Color.cyan;

        if (blockNum == 6)
            transform.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    //public void Block1Con()
    //{
    //    blockNum = 1;
    //    transform.GetComponent<MeshRenderer>().material.color = Color.white;
    //}

    //public void Block2Con()
    //{
    //    blockNum = 2;
    //    transform.GetComponent<MeshRenderer>().material.color = Color.red;
    //}

    //public void Block3Con()
    //{
    //    blockNum = 3;
    //    transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    //}
}
