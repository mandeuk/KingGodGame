using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneUIManager : MonoBehaviour {

    public static PlaySceneUIManager instance = null;

    public GameObject canvas;
    GameObject lifebar, lifeillusionbar, minimap;
    GameObject energyUItext, eterUItext, energyeterUI;
    GameObject twitchAPI, dialogUI, twitchbutton, leftcharacterimg;
    GameObject stageTitle;
    Sprite heartimg, nonheartimg;
    public List<GameObject> hpList = new List<GameObject>();
    public List<GameObject> hpillusionList = new List<GameObject>();

    bool firstFadeLoaded;
    
    bool isfading, isDialogActive;
    GameObject fadeimageforUI;
    Color fadeimgcolor;

    private void Awake()
    {
        if (instance)//인스턴스가 생성되어있는가?
        {
            //DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            //DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }
        firstFadeLoaded = false;


        heartimg = Resources.Load("Image/UI/heart_icon", typeof(Sprite)) as Sprite;
        nonheartimg = Resources.Load("Image/UI/nonheart_icon", typeof(Sprite)) as Sprite;


        fadeimgcolor.r = 0;
        fadeimgcolor.b = 0;
        fadeimgcolor.g = 0;
        fadeimgcolor.a = 1;

        isfading = false;
        isDialogActive = false;

        canvas = Instantiate(Resources.Load("Prefabs/UI/Canvas") as GameObject);
        //twitchAPI = Instantiate(Resources.Load("Prefabs/UI/TwitchAPI") as GameObject);
        dialogUI = Instantiate(Resources.Load("Prefabs/UI/Dialog/DialogUI"), canvas.transform) as GameObject;
    }
    // Use this for initialization
    void Start() {
        lifeillusionbar = GameObject.Find("Lifeillusionbar");
        lifebar = GameObject.Find("Lifebar");
        energyeterUI = GameObject.Find("EnergyEterUI");
        energyUItext = GameObject.Find("EnergyUI/Text");
        eterUItext = GameObject.Find("EterUI/Text");
        fadeimageforUI = GameObject.Find("FadeimageforUI");
        //twitchbutton = GameObject.Find("TwitchButton(Clone)");
        minimap = GameObject.Find("Minimap");
        stageTitle = GameObject.Find("StageTitle");
        stageTitle.SetActive(false);
        leftcharacterimg = GameObject.Find("LeftCharacterImg");
        leftcharacterimg.SetActive(false);
        dialogUI.SetActive(false);

        ChangeEnergyAmountText();
        ChangeEterAmountText();

        for (int i = 0; i < 10; ++i)
        {
            hpList.Add(GenerateHPicon());
            hpillusionList.Add(GenerateHPillusion());
            hpillusionList[i].SetActive(true);
            hpillusionList[i].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
        }
        UpdateHPUI();

        if (firstFadeLoaded == false)
        {
            PlayerBase.instance.PlayerDisable();
            UIFadeIn();
            firstFadeLoaded = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (isDialogActive == true)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
            {
                //스테이지 타이틀 페이드인/아웃 연속으로 + 내부에서 UI ON
                StartCoroutine(ImageFadeInandOut(stageTitle, 0.7f, 2.0f, 2.5f, 1.0f));

                //캐릭터 대화창 끄기
                StartCoroutine(DialogHide());

                //캐릭터가 움직일 수 있게
                PlayerBase.instance.PlayerEnable();
            }
        }
    }

    public void UpdateHPUI()
    {
        for (int loop = 0; loop < hpList.Count; ++loop)
        {
            if (loop < (int)PlayerBase.instance.maxHP)
            {
                hpList[loop].SetActive(true);
                hpillusionList[loop].SetActive(true);
                //hpillusionList[loop].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                hpList[loop].SetActive(false);
                hpillusionList[loop].SetActive(false);
                //hpillusionList[loop].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
            }
        }

        for (int loop = (int)PlayerBase.instance.maxHP - 1; loop >= 0; --loop)
        {
            if (loop > (int)PlayerBase.instance.curHP - 1)
            {
                hpList[loop].GetComponent<UnityEngine.UI.Image>().sprite = nonheartimg;
                //hpillusionList[loop].SetActive(false);
                hpillusionList[loop].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
            }
            else if (loop <= (int)PlayerBase.instance.curHP - 1)
            {
                hpList[loop].GetComponent<UnityEngine.UI.Image>().sprite = heartimg;
                //hpillusionList[loop].SetActive(true);
                hpillusionList[loop].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public GameObject GenerateHPicon()
    {
        GameObject cloneHP = Instantiate(Resources.Load("Prefabs/UI/Heart"), lifebar.transform) as GameObject;//Items 생성
        cloneHP.SetActive(false);

        UnityEngine.UI.Image cloneHPImage = cloneHP.GetComponent<UnityEngine.UI.Image>();
        cloneHPImage.sprite = heartimg;

        return cloneHP;
    }

    public GameObject GenerateHPillusion()
    {
        GameObject cloneHPillusion = Instantiate(Resources.Load("Prefabs/UI/Heartillusion"), lifeillusionbar.transform) as GameObject;//Items 생성
        cloneHPillusion.SetActive(false);

        return cloneHPillusion;
    }


    public void ChangeEterAmountText()
    {
        UnityEngine.UI.Text etertext = eterUItext.GetComponent<UnityEngine.UI.Text>();
        etertext.text = PlayerBase.instance.etere.ToString();
    }
    public void ChangeEnergyAmountText()
    {
        UnityEngine.UI.Text energytext = energyUItext.GetComponent<UnityEngine.UI.Text>();
        energytext.text = PlayerBase.instance.energy.ToString();
    }

    public void HideUI()
    {
        //false
        lifebar.SetActive(false);
        lifeillusionbar.SetActive(false);
        energyeterUI.SetActive(false);
        //twitchbutton.SetActive(false);
        minimap.SetActive(false);
    }

    public void ShowUI()
    {
        //true
        lifebar.SetActive(true);
        lifeillusionbar.SetActive(true);
        energyeterUI.SetActive(true);
        //twitchbutton.SetActive(true);
        minimap.SetActive(true);
    }



    public void UIFadeIn()
    {
        if (isfading == false)
        {
            StartCoroutine(FadeIn());
        }
    }
    public void UIFadeOut()
    {
        if (isfading == false)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        HideUI();
        yield return new WaitForSeconds(0.2f);

        if (isfading == false)
            isfading = true;

        float timer;
        timer = 0.0f;
        fadeimageforUI.SetActive(true);
        while (fadeimgcolor.a > 0f)
        {
            timer += Time.deltaTime / 3;
            fadeimgcolor.a = Mathf.Lerp(1.0f, 0.0f, timer);
            fadeimageforUI.GetComponent<UnityEngine.UI.Image>().color = fadeimgcolor;

            yield return null;
        }
        //yield return new WaitForSeconds(1.0f);
        fadeimageforUI.SetActive(false);
        isfading = false;

        yield return new WaitForSeconds(0.5f);
        DialogShow();
        
        isDialogActive = true;
        yield break;
    }

    IEnumerator FadeOut()
    {
        //HideUI();
        //twitchbutton.SetActive(false);//게임종료할대만 호출되는 Enumerator라 선언해놓은상태

        yield return new WaitForSeconds(0.2f);

        if (isfading == false)
            isfading = true;

        float timer;
        timer = 0.0f;

        fadeimgcolor.a = 0f;
        fadeimageforUI.SetActive(true);
        while (fadeimgcolor.a < 1f)
        {
            timer += Time.deltaTime / 3;
            fadeimgcolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
            fadeimageforUI.GetComponent<UnityEngine.UI.Image>().color = fadeimgcolor;

            yield return null;
        }
        isfading = false;
        
        yield break;
    }

    IEnumerator ImageFadein(GameObject image, float intime)
    {
        image.SetActive(true);
        float timer = 0f;
        Color tempcolor;
        tempcolor.r = 1f;
        tempcolor.g = 1f;
        tempcolor.b = 1f;
        tempcolor.a = 0f;
        image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;
        while (image.GetComponent<UnityEngine.UI.Image>().color.a < 1f)
        {
            timer += Time.deltaTime/intime;
            tempcolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
            image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

            yield return null;
        }

        yield break;
    }

    IEnumerator ImageFadeout(GameObject image, float outtime)
    {
        float timer = 0f;
        Color tempcolor;
        tempcolor.r = 1f;
        tempcolor.g = 1f;
        tempcolor.b = 1f;
        tempcolor.a = 1f;
        image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;
        while (image.GetComponent<UnityEngine.UI.Image>().color.a > 0f)
        {
            timer += Time.deltaTime/outtime;
            tempcolor.a = Mathf.Lerp(1.0f, 0.0f, timer);
            image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

            yield return null;
        }
        image.SetActive(false);
        yield break;
    }

    IEnumerator ImageFadeInandOut(GameObject image, float introDelay, float intime, float maintain, float outtime)
    {
        yield return new WaitForSeconds(introDelay);

        image.SetActive(true);

        float timer = 0f;
        Color tempcolor;
        tempcolor.r = 1f;
        tempcolor.g = 1f;
        tempcolor.b = 1f;
        tempcolor.a = 0f;
        image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;
        while (image.GetComponent<UnityEngine.UI.Image>().color.a < 1f)
        {
            timer += Time.deltaTime/intime;
            tempcolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
            image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

            yield return null;
        }


        //3초동안 보이도록 유지
        yield return new WaitForSeconds(maintain);


        timer = 0f;
        tempcolor.r = 1f;
        tempcolor.g = 1f;
        tempcolor.b = 1f;
        tempcolor.a = 1f;
        image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;
        while (image.GetComponent<UnityEngine.UI.Image>().color.a > 0f)
        {
            timer += Time.deltaTime/outtime;
            tempcolor.a = Mathf.Lerp(1.0f, 0.0f, timer);
            image.GetComponent<UnityEngine.UI.Image>().color = tempcolor;

            yield return null;
        }
        image.SetActive(false);

        //UI화면 보이도록
        ShowUI();

        yield break;
    }

    void DialogShow()
    {
        dialogUI.SetActive(true);
        isDialogActive = true;
        //leftcharacterimg.SetActive(true);
        StartCoroutine(ImageFadein(leftcharacterimg, 0.2f));
        
    }

    IEnumerator DialogHide()
    {
        StartCoroutine(ImageFadeout(leftcharacterimg, 0.2f));
        //leftcharacterimg.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        dialogUI.SetActive(false);
        isDialogActive = false;
    }
}
