using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUIManager : MonoBehaviour {

    public static PlaySceneUIManager instance = null;

    public GameObject canvas;
    GameObject lifebar, lifeillusionbar;
    GameObject minimap, fullmap;
    GameObject energyUItext, eterUItext, energyeterUI;
    GameObject twitchAPI, dialogUI, twitchbutton, leftcharacterimg;
    GameObject stageTitle;
    GameObject encroachment;
    GameObject live2dimg, live2dcam;
    GameObject bosslifebar;

    GameObject votedialog;
    public Image cooltimeGauge;
    Sprite heartimg, nonheartimg, stage1title, stage2title;
    public List<GameObject> hpList = new List<GameObject>();
    public List<GameObject> hpillusionList = new List<GameObject>();

    bool firstFadeLoaded;

    bool isfading, isDialogActive;
    GameObject fadeimageforUI;
    Color fadeimgcolor;

    Text etertext;
    Text energytext;
    
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
        stage1title = Resources.Load("Image/Sprite/StageTitle/abyss", typeof(Sprite)) as Sprite;
        stage2title = Resources.Load("Image/Sprite/StageTitle/freezingPoint", typeof(Sprite)) as Sprite;


        fadeimgcolor.r = 0;
        fadeimgcolor.b = 0;
        fadeimgcolor.g = 0;
        fadeimgcolor.a = 1;

        isfading = false;
        isDialogActive = true;

        canvas = Instantiate(Resources.Load("Prefabs/UI/Canvas") as GameObject);
        twitchAPI = Instantiate(Resources.Load("Prefabs/UI/TwitchAPI") as GameObject);

        dialogUI = Instantiate(Resources.Load("Prefabs/UI/Dialog/DialogUI"), canvas.transform) as GameObject;
        votedialog = Instantiate(Resources.Load("Prefabs/UI/Dialog/VoteDialog"), canvas.transform) as GameObject;

        live2dcam = Instantiate(Resources.Load("Prefabs/UI/CameraForLive2d") as GameObject);
        live2dimg = Instantiate(Resources.Load("Prefabs/UI/CanvasForLive2d") as GameObject);


    }

    // Use this for initialization
    void Start()
    {
        //하이어라키 순서대로 차일드의 게임오브젝트 불러오기
        //새로운 오브젝트를 추가할 경우 반드시 순서대로 코드를 추가할 것
        lifeillusionbar = canvas.transform.GetChild(0).gameObject;
        lifebar = canvas.transform.GetChild(1).gameObject;
        energyeterUI = canvas.transform.GetChild(2).gameObject;
        energyUItext = energyeterUI.transform.GetChild(0).GetChild(1).gameObject;
        eterUItext = energyeterUI.transform.GetChild(1).GetChild(1).gameObject;
        minimap = canvas.transform.GetChild(3).gameObject;
        stageTitle = canvas.transform.GetChild(4).gameObject;
        stageTitle.SetActive(false);
        fullmap = canvas.transform.GetChild(5).gameObject;
        encroachment = canvas.transform.GetChild(6).gameObject;
        cooltimeGauge = canvas.transform.GetChild(7).gameObject.GetComponent<Image>();
        bosslifebar = canvas.transform.GetChild(8).gameObject;
        bosslifebar.SetActive(false);
        fadeimageforUI = canvas.transform.GetChild(9).gameObject;
        twitchbutton = canvas.transform.GetChild(10).gameObject;//수정필요


        dialogUI.SetActive(false);
        votedialog.SetActive(false);
        live2dimg.SetActive(false);//라이브2d이미지

        //leftcharacterimg = GameObject.Find("LeftCharacterImg");
        //leftcharacterimg.SetActive(false);//일러스트이미지


        etertext = eterUItext.GetComponent<UnityEngine.UI.Text>();
        energytext = energyUItext.GetComponent<UnityEngine.UI.Text>();

        //현재 에너지/에테르 수치를 기반으로 화면UI에 표시되는 숫자 갱신
        ChangeEnergyAmountText();
        ChangeEterAmountText();

        //체력 아이콘 생성하는 코드
        for (int i = 0; i < 10; ++i)
        {
            hpList.Add(GenerateHPicon());
            hpillusionList.Add(GenerateHPillusion());
            hpillusionList[i].SetActive(true);
            hpillusionList[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        UpdateHPUI();

        //미니맵 갱신
        if (!GameManager.instance.testMod)
            UpdateMap();

        //쿨타임게이지 위치갱신
        StartCoroutine(CooltimeSetPos());

        //폭주게이지 초기 갱신
        EncroachmentManager.instance.UpdateDevilGauge();

        //
        if (firstFadeLoaded == false)
        {
            if (!GameManager.instance.testMod)
            {
                switch (GameManager.playerPosition.stageNum)
                {
                    case 1:
                        UIFadeIn(true);//페이드인
                        firstFadeLoaded = true;//스테이지 시작 다이얼로그가 출력되었다고 표시
                        break;
                    case 2:
                        UIFadeIn(false);//페이드인
                        firstFadeLoaded = true;//스테이지 시작 다이얼로그가 출력되었다고 표시
                        break;
                }
            }
            else
            {
                UIFadeIn(false);//페이드인
                firstFadeLoaded = true;//스테이지 시작 다이얼로그가 출력되었다고 표시
            }
        }

        //스테이지별 타이틀 문구 결정
        switch (GameManager.playerPosition.stageNum)
        {
            case 1:
                stageTitle.GetComponent<Image>().sprite = stage1title;
                break;
            case 2:
                stageTitle.GetComponent<Image>().sprite = stage2title;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        if (isDialogActive == true)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || (Input.GetKeyDown(KeyCode.J)))
            {
                if (DialogManager.instance.NextDialog())
                {
                }
                else
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

        if (isDialogActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.M))
            {
                bool isactive;
                isactive = fullmap.active;
                //ShowFullmap();

                fullmap.SetActive(!isactive);
                FullmapManager.instance.UpdateStatusText();
            }
        }
    }
    
    public void UpdateMap()
    {
        MinimapManager.instance.UpdateMinimap(minimap);
        FullmapManager.instance.UpdateFullmap(fullmap);
    }

    public void UpdateHPUI()
    {
        for (int loop = 0; loop < hpList.Count; ++loop)
        {
            if (loop < (int)PlayerBase.instance.maxHP)
            {
                hpList[loop].SetActive(true);
                //hpillusionList[loop].SetActive(true);//이 코드를 쓰면 체력 회복했을 때 일루미네이션 효과 애니메이션이 기존 체력 일루미네이션들과 싱크가 맞지 않음
                hpillusionList[loop].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                hpList[loop].SetActive(false);
                //hpillusionList[loop].SetActive(false);
                hpillusionList[loop].GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
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
        etertext.text = PlayerBase.instance.etere.ToString();
    }
    public void ChangeEnergyAmountText()
    {
        energytext.text = PlayerBase.instance.energy.ToString();
    }

    public void HideUI()
    {
        //false
        lifebar.SetActive(false);
        lifeillusionbar.SetActive(false);
        energyeterUI.SetActive(false);
        twitchbutton.SetActive(false);
        minimap.SetActive(false);
        fullmap.SetActive(false);
        encroachment.SetActive(false);

        //혹시 보스게이지가 활성화 되어 있다면
        if(bosslifebar.active == true)
            bosslifebar.SetActive(false);
    }

    public void ShowUI()
    {
        //true
        lifebar.SetActive(true);
        lifeillusionbar.SetActive(true);
        energyeterUI.SetActive(true);
        if (TwitchChat.Instance.IsTwitchConnected() == true)
        {
            twitchbutton.SetActive(true);
        }
        minimap.SetActive(true);
        encroachment.SetActive(true);
    }



    public void UIFadeIn(bool dialogactive)
    {
        if (isfading == false)
        {
            if (dialogactive == true)
            {
                PlayerBase.instance.PlayerDisable();//플레이어 조작 안되도록
            }
            StartCoroutine(FadeIn(dialogactive));
        }
    }
    public void UIFadeOut()
    {
        if (isfading == false)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn(bool dialogactive)
    {
        HideUI();
        // getChild 안쓰는거로 해야됨.
        //Camera.main.transform.GetChild(0).gameObject.SetActive(false);
        //Camera.main.transform.GetChild(1).gameObject.SetActive(false);
        //Camera.main.transform.GetChild(2).gameObject.SetActive(false);


        yield return new WaitForSeconds(0.2f);

        //Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        //Camera.main.transform.GetChild(1).gameObject.SetActive(true);
        //Camera.main.transform.GetChild(2).gameObject.SetActive(true);

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

        if (dialogactive == true)
        {
            DialogShow(GameManager.playerPosition.stageNum);//첫 시작시 대사
            isDialogActive = true;
        }
        else
        {
            //스테이지 타이틀 페이드인/아웃 연속으로 + 내부에서 UI ON
            StartCoroutine(ImageFadeInandOut(stageTitle, 0.7f, 2.0f, 2.5f, 1.0f));

            //캐릭터 대화창 끄기
            StartCoroutine(DialogHide());
        }
        yield break;
    }

    IEnumerator FadeOut()
    {
        HideUI();
        twitchbutton.SetActive(false);//게임종료할대만 호출되는 Enumerator라 선언해놓은상태

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

        //Camera.main.transform.GetChild(2).gameObject.SetActive(false);
        //Camera.main.transform.GetChild(1).gameObject.SetActive(false);
        //Camera.main.transform.GetChild(0).gameObject.SetActive(false);

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
        image.GetComponent<Image>().color = tempcolor;
        while (image.GetComponent<Image>().color.a < 1f)
        {
            timer += Time.deltaTime/intime;
            tempcolor.a = Mathf.Lerp(0.0f, 1.0f, timer);
            image.GetComponent<Image>().color = tempcolor;

            yield return null;
        }


        //3초동안 보이도록 유지
        yield return new WaitForSeconds(maintain);


        timer = 0f;
        tempcolor.r = 1f;
        tempcolor.g = 1f;
        tempcolor.b = 1f;
        tempcolor.a = 1f;
        image.GetComponent<Image>().color = tempcolor;
        while (image.GetComponent<Image>().color.a > 0f)
        {
            timer += Time.deltaTime/outtime;
            tempcolor.a = Mathf.Lerp(1.0f, 0.0f, timer);
            image.GetComponent<Image>().color = tempcolor;

            yield return null;
        }
        image.SetActive(false);

        //UI화면 보이도록
        ShowUI();

        yield break;
    }

    public void DialogShow(int dialognum)
    {
        dialogUI.SetActive(true);//대사창Base, Text보이기
        isDialogActive = true;//대화창이 켜져있는지 확인여부
        live2dimg.SetActive(true);//라이브2D ON
        //leftcharacterimg.SetActive(true);
        //StartCoroutine(ImageFadein(leftcharacterimg, 0.2f));//일러스트 이미지

        //대화창 텍스트 내용 바꾸기
        //if (dialogtext != null)
            //dialogUI.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = dialogtext;
        DialogManager.instance.StartDialog(dialognum, dialogUI.transform.GetChild(1).gameObject.transform.GetChild(0));
    }

    IEnumerator DialogHide()
    {
        //StartCoroutine(ImageFadeout(leftcharacterimg, 0.2f));//일러스트 이미지
        //leftcharacterimg.SetActive(false);

        yield return new WaitForSeconds(0.3f);
        live2dimg.SetActive(false);
        dialogUI.SetActive(false);
        isDialogActive = false;
    }

    public void ShowFullmap()
    {
        fullmap.SetActive(true);
    }

    public void StartCooltimeGauge()
    {
        StartCoroutine(CooltimeProgress());
    }

    IEnumerator CooltimeProgress()
    {
        cooltimeGauge.transform.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime / 6;
            cooltimeGauge.fillAmount = Mathf.Lerp(0.0f, 1.0f, timer);
            yield return null;
        }

        cooltimeGauge.transform.gameObject.SetActive(false);

        yield break;
    }

    IEnumerator CooltimeSetPos()
    {
        while(true)
        {
            Vector3 positionsupport;
            positionsupport.x = 0.7f;
            positionsupport.y = 0.9f;
            positionsupport.z = -0.7f;
            Vector3 gaugePos = Camera.main.WorldToScreenPoint(PlayerBase.instance.transform.position + positionsupport);
            cooltimeGauge.transform.SetPositionAndRotation(gaugePos, new Quaternion());
            yield return new WaitForEndOfFrame();//yield return null로 할 경우 좌표가 튀며 게이지 UI가 잔상을남기며 진동함
        }
        yield break;
    }

    public void ShowVoteDialog(string dialogtext)
    {
        StartCoroutine(VoteDialog(dialogtext));
    }

    IEnumerator VoteDialog(string dialogtext)
    {
        if(dialogtext != null)
            votedialog.transform.GetChild(0).GetComponent<Text>().text = dialogtext;
        votedialog.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        votedialog.SetActive(false);

        yield break;
    }

}
