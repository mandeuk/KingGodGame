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

    private void Start()
    {
        leftDoorCol.GetComponent<DoorCol>().doorType = 0;
        frontDoorCol.GetComponent<DoorCol>().doorType = 1;
        rightDoorCol.GetComponent<DoorCol>().doorType = 2;
        backDoorCol.GetComponent<DoorCol>().doorType = 3;
    }

    public void LeftDoorOpen()
    {
        leftWallCol.SetActive(false);
        leftDoorCol.SetActive(true);
    }
    public void RightDoorOpen()
    {
        rightWallCol.SetActive(false);
        rightDoorCol.SetActive(true);
    }
    public void FrontDoorOpen()
    {
        frontWallCol.SetActive(false);
        frontDoorCol.SetActive(true);
    }
    public void BackDoorOpen()
    {
        backWallCol.SetActive(false);
        backDoorCol.SetActive(true);
    }
}
