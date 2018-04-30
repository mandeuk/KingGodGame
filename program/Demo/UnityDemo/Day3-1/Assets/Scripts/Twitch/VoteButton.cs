using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void VoteStartFunction()
    {
        //print("투표버튼 클릭");
        if (TwitchChat.Instance.votestart)
            TwitchChat.Instance.StopVote();
        else
            TwitchChat.Instance.StartVote();
    }
}
