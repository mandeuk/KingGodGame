using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroVideoManager : MonoBehaviour {
    UnityEngine.Video.VideoPlayer introvideo;

    public GameObject logoimage;
    public GameObject gamestartBtn;

    bool videohasplayed;
    // Use this for initialization

    private void Awake()
    {
        
    }

    void Start () {
        introvideo = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videohasplayed = false;
        logoimage.SetActive(false);
        gamestartBtn.SetActive(false);
        introvideo.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if(introvideo.isPlaying)
        {
            videohasplayed = true;
        }
        else
        {
            StartCoroutine(DisableVideo());
        }
	}

    IEnumerator DisableVideo()
    {
        if ((introvideo.isPlaying == false) && videohasplayed)
        {
            logoimage.SetActive(true);//로고이미지 띄우기
            gamestartBtn.SetActive(true);//플레이버튼 띄우기
            gameObject.SetActive(false);//동영상 안보이게하기
            yield break;
        }
    }
}
