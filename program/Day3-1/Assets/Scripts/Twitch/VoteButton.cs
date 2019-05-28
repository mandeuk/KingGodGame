using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteButton : MonoBehaviour {

    public static VoteButton instance = null;

    private void Awake()
    {
        if (instance)//인스턴스가 생성되어있는가?
        {
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
        }
    }

    public void VoteStartFunction()
    {
        if (TwitchChat.Instance.votestart)
        {
            TwitchChat.Instance.StopVote();
            TwitchChat.Instance.DeleteVoteTextAll();
            TwitchChat.Instance.SendChatMessage("플레이어에 의해 투표가 취소되었습니다.");
        }
        else
            TwitchChat.Instance.StartVote();
    }
}
