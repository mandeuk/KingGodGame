using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColManager : MonoBehaviour {
    public GameObject leftWallCol;
    public GameObject rightWallCol;
    public GameObject frontWallCol;
    public GameObject backWallCol;

    public GameObject leftDoorCol;
    public GameObject rightDoorCol;
    public GameObject frontDoorCol;
    public GameObject backDoorCol;

    void Awake () {
        
    }

    private void Start()
    {
        leftDoorCol.GetComponent<DoorCol>().doorType = 0;
        frontDoorCol.GetComponent<DoorCol>().doorType = 1;
        rightDoorCol.GetComponent<DoorCol>().doorType = 2;
        backDoorCol.GetComponent<DoorCol>().doorType = 3;
    }

    private void Update()
    {

    }

    public void LeftDoorOpen()
    {
        leftWallCol.SetActive(false);
    }
    public void RightDoorOpen()
    {
        rightWallCol.SetActive(false);
    }
    public void FrontDoorOpen()
    {
        frontWallCol.SetActive(false);
    }
    public void BackDoorOpen()
    {
        backWallCol.SetActive(false);
    }

    public void JudgePlayerInNextRoom()
    {
        if (leftDoorCol.GetComponent<DoorCol>().playerInDoor)
        {
            print("LeftIn");
        }
        else if (rightDoorCol.GetComponent<DoorCol>().playerInDoor)
        {
            print("rightIn");
        }
        else if (frontDoorCol.GetComponent<DoorCol>().playerInDoor)
        {
            print("FrontIn");
        }
        else if (backDoorCol.GetComponent<DoorCol>().playerInDoor)
        {
            print("BackIn");
        }
    }
}
