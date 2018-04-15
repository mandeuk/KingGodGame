using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour {
    public GameObject roomNotClearCol;
    bool roomStateIsClear;


	// Use this for initialization
	void Awake () {
        roomStateIsClear = false;
        RoomStart();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void RoomClear()
    {
        roomStateIsClear = true;

        // 문이 열려야함
        transform.GetComponent<Animator>().SetBool("DoorOpen",true);
        roomNotClearCol.SetActive(false);
        // 콜라이더가 바뀌어야함

    }

    public void RoomStart()
    {
        transform.GetComponent<Animator>().SetBool("DoorOpen", false);
    }
}
