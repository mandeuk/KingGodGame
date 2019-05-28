using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestartBtn : MonoBehaviour {
    //public AudioSource source;
    //public AudioClip click;
    public GameObject fadeimageforUI;
    float timer;
    Color fadeimgcolor;

    private void Awake()
    {
        fadeimageforUI.SetActive(false);//페이드인/아웃 전용 UI이미지 비활성화
        fadeimgcolor.r = 0;
        fadeimgcolor.b = 0;
        fadeimgcolor.g = 0;
        fadeimgcolor.a = 0;
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadGameplayScene()
    {
        //GameObject.Find("Main Camera").GetComponent
        GameManager.NewGameStart();//캐릭터 능력치 초기화
        StartCoroutine(LoadSceneWithFadeOut());
        gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    IEnumerator LoadSceneWithFadeOut()
    {
        timer = 0.0f;
        fadeimageforUI.SetActive(true);
        while(fadeimgcolor.a < 1f)
        {
            timer += Time.deltaTime;
            fadeimgcolor.a = Mathf.Lerp(0.0f, 1.0f, timer); 
            fadeimageforUI.GetComponent<UnityEngine.UI.Image>().color = fadeimgcolor;

            if (fadeimgcolor.a >= 1f)
                LoadingSceneManager.LoadScene("Game_Stage1");
            yield return null;
        }
        //yield return new WaitForSeconds(1.0f);

        yield break;
    }

    //public void OnClickSound()
    //{
    //    source.PlayOneShot(click);
    //}
}
