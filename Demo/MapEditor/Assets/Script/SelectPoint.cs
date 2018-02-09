using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPoint : MonoBehaviour {

    public List<GameObject> objectList = new List<GameObject>();
    bool selectBlock = false;

    public Block blocks;

    private void OnMouseDown()
    {
        selectBlock = !selectBlock;
    }

    // Use this for initialization
    void Start () {
        //blocks = gameObject.GetComponent<Block>();
    }
	
	// Update is called once per frame
	void Update () {
        if (selectBlock)
        {
            transform.GetComponent<Block>().selectBlockClone = true;

        }
        else
        {
            transform.GetComponent<Block>().selectBlockClone = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.GetComponent<Block>().blockNum = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.GetComponent<Block>().blockNum = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.GetComponent<Block>().blockNum = 3;
        }
    }
}

