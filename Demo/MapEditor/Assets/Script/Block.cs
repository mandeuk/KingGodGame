using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour {
    public bool selectBlockClone;
    public int blockNum;

    Block blockClones;

    // Use this for initialization
    void Start () {
        blockNum = 1;
        selectBlockClone = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (selectBlockClone)
        {
            SelectObjectBlock();
        }

        else
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.GetComponent<Block>().blockNum = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            this.GetComponent<Block>().blockNum = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.GetComponent<Block>().blockNum = 3;
        }
    }

    public void SelectObjectBlock()
    {
        if (blockNum == 1)
            transform.GetComponent<MeshRenderer>().material.color = Color.white;

        if (blockNum == 2)
            transform.GetComponent<MeshRenderer>().material.color = Color.red;

        if (blockNum == 3)
            transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public void Block1Con()
    {
        blockNum = 1;
        transform.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void Block2Con()
    {
        blockNum = 2;
        transform.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void Block3Con()
    {
        blockNum = 3;
        transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
