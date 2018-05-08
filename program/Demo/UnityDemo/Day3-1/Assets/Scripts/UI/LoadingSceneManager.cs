using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneManager : MonoBehaviour {

    public static string nextScene; //불러올 씬의 이름을 저장
    public GameObject FadeEffectIMG;

    Color fadecolor;
    float timer;


    [SerializeField]
    Image progressBar;

    private void Awake()
    {
        fadecolor.r = 0;
        fadecolor.g = 0;
        fadecolor.b = 0;
        fadecolor.a = 1f;
        StartCoroutine(FadeIn());
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator FadeIn()
    {
        timer = 0.0f;
        
        
        while (fadecolor.a > 0f)
        {
            timer += Time.deltaTime*2;
            fadecolor.r = 0f;
            fadecolor.g = 0f;
            fadecolor.b = 0f;
            fadecolor.a = Mathf.Lerp(1.0f, 0.0f, timer);
            FadeEffectIMG.GetComponent<UnityEngine.UI.Image>().color = fadecolor;
            if (fadecolor.a <= 0f)
            {
                StartCoroutine(LoadScene());
            }
            yield return null;
        }
        yield break;
    }

    IEnumerator FadeOut()
    {
        timer = 0.0f;
        while (fadecolor.a < 1f)
        {
            timer += Time.deltaTime*2;
            fadecolor.r = 0f;
            fadecolor.g = 0f;
            fadecolor.b = 0f;
            fadecolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
            FadeEffectIMG.GetComponent<Image>().color = fadecolor;
            yield return null;
        }
        yield break;
    }

    IEnumerator LoadScene()
    {
        print("로드씬");
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if(op.progress >= 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);
                
                if (progressBar.fillAmount == 1.0f)
                {
                    if (timer > 1.0f)
                    {
                        StartCoroutine(FadeOut());
                        yield return new WaitForSeconds(1.0f);
                        op.allowSceneActivation = true;
                    }
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0.0f;
                }
            }
        }
    }
}
