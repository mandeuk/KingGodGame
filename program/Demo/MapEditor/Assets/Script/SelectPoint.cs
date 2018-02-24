using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPoint : MonoBehaviour {
    
    bool selectBlock = false;
    int selectBlockNum;

    private void OnMouseEnter()
    {
        if (selectBlock) {
            if (selectBlockNum == 1)
            {
                transform.GetComponent<Block>().blockNum = 1;
            }

            else if (selectBlockNum == 2)
            {
                transform.GetComponent<Block>().blockNum = 2;
            }

            else if (selectBlockNum == 3)
            {
                transform.GetComponent<Block>().blockNum = 3;
            }

            else if (selectBlockNum == 4)
            {
                transform.GetComponent<Block>().blockNum = 4;
            }

            else if (selectBlockNum == 5)
            {
                transform.GetComponent<Block>().blockNum = 5;
            }

            else if (selectBlockNum == 6)
            {
                transform.GetComponent<Block>().blockNum = 6;
            }
        }
    }

    // Use this for initialization
    void Start () {
        selectBlockNum = 1;
    }
	
	// Update is called once per frame
	void Update () {

        selectBlock = false;

        if (Input.GetKeyDown(KeyCode.A))
        {
            selectBlockNum = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            selectBlockNum = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            selectBlockNum = 3;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectBlockNum = 4;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            selectBlockNum = 5;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            selectBlockNum = 6;
        }

        if (Input.GetMouseButton(0))
        {
            selectBlock = true;
        }
    }
}

