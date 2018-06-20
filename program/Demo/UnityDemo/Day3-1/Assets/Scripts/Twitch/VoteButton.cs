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
        if (TwitchChat.Instance.votestart)
        {
            TwitchChat.Instance.StopVote();
            TwitchChat.Instance.DeleteVoteTextAll();
        }
        else
            TwitchChat.Instance.StartVote();
    }
}
