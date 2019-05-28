using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroVideoManager : MonoBehaviour {
    UnityEngine.Video.VideoPlayer introvideo;
    public GameObject introvideoObject;
    public GameObject logoimage, logoflareimage, backgroundimage;
    public GameObject gamestartBtn, exitBtn;

    bool videohasplayed;
    bool isMunupopup;
    // Use this for initialization

    private void Awake()
    {
        
    }

    void Start () {
        introvideo = introvideoObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videohasplayed = false;
        isMunupopup = false;
        logoimage.SetActive(false);
        logoflareimage.SetActive(false);
        backgroundimage.SetActive(false);
        gamestartBtn.SetActive(false);
        exitBtn.SetActive(false);
        introvideo.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (introvideo.isPlaying)
        {
            videohasplayed = true;
        }
        if(videohasplayed == true)
        {
            if ((introvideo.isPlaying != true) && (isMunupopup == false))
            {
                isMunupopup = true;
                StartCoroutine(DisableVideo());
            }
        }


        
    }

    IEnumerator DisableVideo()
    {
        if ((introvideo.isPlaying == false) && videohasplayed)
        {
            introvideoObject.SetActive(false);//동영상 안보이게하기
            logoimage.SetActive(true);//로고이미지 띄우기
            StartLogoAnimation();
            yield break;
        }
    }

    void StartLogoAnimation()
    {
        StartCoroutine(TitleLogoAnimation());
        StartCoroutine(TitleImgFlareEffect());
    }

    IEnumerator TitleLogoAnimation()
    {
        
        float timer = 0.0f, originalYpos = 0.0f, targetYpos = 0.0f, originalflareYpos, targetflareYpos;
        
        //로고 이미지 위치이동을 위한 코드
        Vector3 imagepos = logoimage.transform.position;
        originalYpos = imagepos.y;
        targetYpos = imagepos.y + (Screen.height / 7.0f);

        //로고 이미지 위치이동을 위한 코드
        Vector3 flarepos = logoflareimage.transform.position;
        originalflareYpos = flarepos.y;
        targetflareYpos = flarepos.y + (Screen.height / 7.0f);

        //배경 이미지 알파값 조절을 위한 코드
        backgroundimage.SetActive(true);//백그라운드 이미지 띄우기
        Color tempcolor;
        tempcolor.r = 1.0f;
        tempcolor.g = 1.0f;
        tempcolor.b = 1.0f;
        tempcolor.a = 0.0f;
        backgroundimage.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

        while (timer < 1.0f)
        {
            timer += Time.deltaTime/3;
            
            //로고이미지
            imagepos.y = originalYpos + (Mathf.Lerp(0.0f, 1.0f, timer) * (Screen.height / 7.0f));
            logoimage.transform.position = imagepos;

            //플레어이미지
            flarepos.y = originalflareYpos + (Mathf.Lerp(0.0f, 1.0f, timer) * (Screen.height / 7.0f));
            logoflareimage.transform.position = flarepos;

            //배경이미지
            tempcolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
            backgroundimage.GetComponent<UnityEngine.UI.Image>().color = tempcolor;
            yield return null;
        }

        StopCoroutine(TitleLogoAnimation());
        
        gamestartBtn.SetActive(true);//플레이버튼 띄우기
        exitBtn.SetActive(true);//게임 종료 버튼 띄우기
        yield break;
    }
    
    IEnumerator TitleImgFlareEffect()
    {
        bool afterFirstLoop = false;
        float timer = 0.0f;

        //배경 이미지 알파값 조절을 위한 코드
        logoflareimage.SetActive(true);//백그라운드 이미지 띄우기
        Color tempcolor;
        tempcolor.r = 1.0f;
        tempcolor.g = 1.0f;
        tempcolor.b = 1.0f;
        tempcolor.a = 0.0f;
        logoflareimage.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

        while (true)
        {
            while(tempcolor.a < 1.0f)
            {
                timer += Time.deltaTime;

                if(afterFirstLoop == false)
                    tempcolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
                else
                    tempcolor.a = Mathf.Lerp(0.3f, 1.0f, timer);
                logoflareimage.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

                yield return null;
            }
            timer = 0.0f;
            while (tempcolor.a > 0.3f)
            {
                timer += Time.deltaTime;

                tempcolor.a = Mathf.Lerp(1.0f, 0.3f, timer);
                logoflareimage.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

                yield return null;
            }
            timer = 0.0f;
            afterFirstLoop = true;
        }
    }
}
