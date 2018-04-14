using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour {

    bool roomState;

	// Use this for initialization
	void Awake () {
        transform.GetComponent<Animator>().speed = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RoomClear()
    {
        // 문이 열려야함
        transform.GetComponent<Animator>().speed = 1;

        // 콜라이더가 바뀌어야함
    }
}
