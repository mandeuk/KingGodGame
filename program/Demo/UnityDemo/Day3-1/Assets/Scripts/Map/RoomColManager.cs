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


}
