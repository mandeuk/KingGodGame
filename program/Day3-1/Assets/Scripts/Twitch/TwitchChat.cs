using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.IO;


public class TwitchChat : MonoBehaviour
{
    //싱글턴으로 만들기 위한 인스턴스 생성
    private static TwitchChat instance = null;

    // IRC(Internet Relay Chat)시스템에서 사용하는 프로토콜은 TCP를 사용한다.
    // 그러므로 TcpClient 클래스를 사용하여 연결한다.
    private static TcpClient twitchClient;
    private static StreamReader reader;
    private static StreamWriter writer;

    public string username, password, channelName; //외부프로그램을 사용한 IRC연결에 사용할 비밀번호는 https://twitchapps.com/tmi 이곳에서 확인할 수 있다. eduuc33x29lk98rr8x46x6mckds2ug
    public bool votestart;
    //bool twitchPlayMode; // 아직 사용안하는중
    public int[] vote = { 0, 0, 0};//투표 횟수를 저장하기 위한 배열 선언
    public int[] product = { 0, 0, 0};
    SortedDictionary<string, string> IDList = new SortedDictionary<string, string>();//SortedDictionary를 사용해 투표한 사용자의 ID를 기록하고 이를 통해 중복체크기능 수행

    GameObject twitchVoteButton;
    GameObject[] voteResultText = { null, null, null };
    GameObject[] voteProductName = { null, null, null };
    GameObject timerText;

    //외부에서 인스턴스를 호출할때 리턴시켜주는 기능
    public static TwitchChat Instance
    {
        get
        {
            return instance;
        }
    }
    
    
    
    // 함수이름 : Start()
    // 함수기능 : 해당 CS스크립트가 실행될 때 딱 한번 내부의 코드를 실행한다. 트위치 접속을 위해 Connect()함수가 있다.
    void Awake()
    {
        //싱글턴 관련 코드
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

        TwitchStart();
    }//end of Start()

    private void Start()
    {
        votestart = false;
    }

    //트위치 연결 코루틴을 실행하는 함수
    void TwitchStart()
    {
        StartCoroutine(TwitchConnect());
    }

    //트위치 IRC서버와 연결하는 코루틴
    IEnumerator TwitchConnect()
    {
        Connect();
        if (twitchClient.Connected)
        {
            SetupTwitchBtn();
        }
        else
        {
            while (twitchClient.Connected == false)
            {
                Connect();//트위치 irc서버에 접속시도
                if (twitchClient.Connected)
                {
                    SetupTwitchBtn();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield break;
    }
    
    //채팅시간갱신 + 채팅읽기 + 최종 투표처리
    public IEnumerator TwitchUpdate()
    {
        float timer = 30.0f;
        float textUpdateTime = 0.0f;

        while(votestart)
        {
            if (!twitchClient.Connected)//만약 TcpClient가 irc서버에 접속하지 못했을 경우 다시 접속 시도
            {
                print("서버접속 실패");
                Connect();//트위치 irc서버에 접속시도
            }
            else
            {
                float deltatime = Time.deltaTime;
                timer -= deltatime;
                textUpdateTime += deltatime;
                if(textUpdateTime >= 1.0f)
                {
                    UpdateVoteTimer((int)timer);
                    textUpdateTime -= 1.0f;
                }

                ReadChat();//만약 irc서버에 접속을 성공했다면 채팅을 읽어들인다.

                if (timer <= 0.0f)
                {
                    if (votestart)
                        ProcessVote();
                    StopVote();
                    print("투표성공");
                }
            }
            
            yield return null;
        }


        //투표 종료 10초 후 텍스트 없애기
        //if (votestart == false)
        //{
        //    yield return new WaitForSeconds(10f);
        //    DeleteVoteTextAll();
        //}
        yield break;
    }//end of TwitchUpdate()


    
    // 함수이름 : Connect()
    // 함수기능 : TcpClient로 트위치 IRC서버에 연결한 후 StreamReader와 StreamWriter를 설정하고 자신의 채널에 접속한다.
    public void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667); //트위치의 IRC 채팅서버 주소와 포트번호, IRC서버는 보통 6667번 포트를 많이 사용한다.
        if (twitchClient.Connected == true)
        {
            reader = new StreamReader(twitchClient.GetStream());
            writer = new StreamWriter(twitchClient.GetStream());

            writer.WriteLine("PASS " + password);
            writer.WriteLine("NICK " + username);
            writer.WriteLine("USER " + username + " 8 * :" + username);
            writer.WriteLine("JOIN #" + channelName);
            writer.WriteLine("PRIVMSG #" + channelName + " :Counterflow와 Twitch채팅서버가 성공적으로 연결되었습니다.");
            writer.Flush();//Flush() = 해당 writer의 버퍼를 clear하는 기능
        }
    }//end of Connect()


    
    // 함수이름 : ReadChat()
    // 함수기능 : Connect()에서 설정한 NetworkStream을 사용해 IRC서버의 채팅내용을 실시간으로 불러들인다.
    //            현재 채팅내역은 유니티 콘솔창에 출력하도록 되어있다. 추후 수정할 예정
    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine();

            //print(message);//콘솔출력
            //////////////////////////////////////////////////////////
            //  트위치 채팅채널에서 채팅을 입력할 경우
            //  :mandeuk!mandeuk@mandeuk.tmi.twitch.tv PRIVMSG #mandeuk :test1
            //  이와 같은문구가 전송되고 여기서 채팅을 한 사람과 채팅 내역을 뽑아내기 위해
            //  앞쪽의 닉네임 문자열을 뽑아내기 위해 IndexOf()함수로 !의 위치를 리턴, splitPoint에 저장
            //  Substring()함수를 사용해 :mandeuk! 특수문자 사이의 mandeuk을 뽑아낸다.
            ///////////////////////////////////////////////////////////
            if (votestart == true)
            {
                if (message.Contains("PRIVMSG"))//개인채팅메시지인 경우
                {
                    var splitPoint = message.IndexOf("!", 1);
                    var chatName = message.Substring(0, splitPoint);
                    chatName = chatName.Substring(1);

                    splitPoint = message.IndexOf(":", 1);
                    message = message.Substring(splitPoint + 1);
                    print(String.Format("{0}: {1}", chatName, message));

                    if (IDList.ContainsKey(chatName))
                    {
                        print("중복닉네임 존재");
                    }
                    else
                    {
                        switch (message)
                        {
                            case "#1":
                                IDList.Add(chatName, chatName);
                                vote[0] += 1;
                                print("1번 투표수" + vote[0]);
                                break;
                            case "#2":
                                IDList.Add(chatName, chatName);
                                vote[1] += 1;
                                print("2번 투표수" + vote[1]);
                                break;
                            case "#3":
                                IDList.Add(chatName, chatName);
                                vote[2] += 1;
                                print("3번 투표수" + vote[2]);
                                break;
                        }
                        UpdateVoteCount();//UI에 표시되는 숫자 변경
                    }
                }//end of if (message.Contains("PRIVMSG"))
            }//end of if (votestart == true)
        }//end of if (twitchClient.Available > 0)
    }// end of ReadChat()



    public void StartVote()
    {
        votestart = true;//채팅을 읽기위해 투표시작을 알리는 변수 변경
        System.Array.Clear(vote, 0, 3);//투표횟수 저장하는 변수를 초기화
        System.Array.Clear(product, 0, 3);//투표 상품을 저장하는 변수를 초기화
        IDList.Clear();//투표 중복체크하는 트리 초기화
        
        for (int loop = 0; loop < 3; loop++)
        {
            product[loop] = UnityEngine.Random.Range(0, 9);
        }

        UpdateVoteProduct();
        UpdateVoteCount();

        StartCoroutine(TwitchUpdate());//투표 코루틴 시작
        SendChatMessage("투표시작");
        SendChatMessage("#1 - " + voteProductName[0].GetComponent<UnityEngine.UI.Text>().text);
        SendChatMessage("#2 - " + voteProductName[1].GetComponent<UnityEngine.UI.Text>().text);
        SendChatMessage("#3 - " + voteProductName[2].GetComponent<UnityEngine.UI.Text>().text);
    }

    public void StopVote()
    {
        //print("StopVote() 투표종료");
        votestart = false;
        StopCoroutine(TwitchUpdate());//투표 코루틴 강제정지
        TwitchChat.Instance.DeleteVoteTextAll();
        //SendChatMessage("플레이어에 의해 투표가 취소되었습니다.");
    }

    public void ProcessVote()
    {
        //print("ProcessVote() 투표결과도출");
        int selected = 0;
        bool randomselect = false;

        if(vote[0] > vote[1])
        {
            if(vote[0] > vote[2]) {
                selected = 0;
            }
            else if (vote[0] == vote[2]) {
                selected = UnityEngine.Random.Range(0, 2);
                if (selected == 1)
                    selected = 2;
                randomselect = true;
            }
            else if (vote[0] < vote[2]) {
                selected = 2;
            }
        }


        else if (vote[0] == vote[1]) {
            if (vote[0] > vote[2]) {
                selected = UnityEngine.Random.Range(0, 2);
                randomselect = true;
            }
            else if (vote[0] == vote[2]) {
                selected = UnityEngine.Random.Range(0, 3);
                randomselect = true;
            }
            else if (vote[0] < vote[2]) {
                selected = 2;
            }
        }


        else if (vote[0] < vote[1]) {
            if (vote[1] > vote[2])
            {
                selected = 1;
            }
            else if (vote[1] == vote[2])
            {
                selected = UnityEngine.Random.Range(1, 3);
                randomselect = true;
            }
            else if (vote[1] < vote[2])
            {
                selected = 2;
            }
        }
        
        {
            string finalitemname = Itemtable.Instance.GetItemName(product[selected]);
            SendChatMessage("투표가 종료되었습니다. " + (selected + 1) + "번 " + finalitemname + "이(가) 선택되었습니다.");
            PlaySceneUIManager.instance.ShowVoteDialog("투표결과 : " + finalitemname);
        }

        //투표 결과 적용부분
        //능력치 수정
        Itemtable.Instance.ApplyItem(product[selected]);
        SoundManager.playTwitchApply();
        PlayerColorChange.instance.PlayerColorChangePurple();
        //이펙트 효과
        //사운드 출력
        //다이얼로그 출력

        //(구) 아이템을 머리 위에 오브젝트로 떨어뜨려주는 기능
        //ItemManager.Instance.SpawnItem(PlayerBase.instance.gameObject.transform.position + new Vector3(0,10,0), product[selected]);


    }//end of ProcessVote()
    
    public void SendChatMessage(string msg)
    {
        //writer = new StreamWriter(twitchClient.GetStream());
        
        writer.WriteLine("PRIVMSG #" + channelName + " :" + msg);

        writer.Flush();
    }

    //
    bool SetupTwitchBtn()
    {
        twitchVoteButton = GameObject.Find("TwitchButton");
        if (twitchVoteButton == null)//같은 이름의 버튼이 있는지 확인
        {
            GameObject canvas = PlaySceneUIManager.instance.canvas; //GameObject.Find("Canvas(Clone)");
            if (canvas != null)
            {
                twitchVoteButton = Instantiate(Resources.Load("Prefabs/UI/TwitchButton"), canvas.transform) as GameObject;//트위치 버튼 생성
            }
            if (twitchVoteButton != null)//버튼 생성에 성공했다면
            {
                voteProductName[0] = GameObject.Find("Product1");//텍스트를 찾아서 수정할 준비
                voteProductName[1] = GameObject.Find("Product2");
                voteProductName[2] = GameObject.Find("Product3");
                voteResultText[0] = GameObject.Find("Product1vote");
                voteResultText[1] = GameObject.Find("Product2vote");
                voteResultText[2] = GameObject.Find("Product3vote");
                timerText = GameObject.Find("VoteTimer");
                return true;
            }
        }
        return false;
    }
    
    //화면 우측에 표시되는 투표지의 아이템 이름 텍스트 갱신
    void UpdateVoteProduct()
    {
        voteProductName[0].GetComponent<UnityEngine.UI.Text>().text = Itemtable.Instance.GetItemName(product[0]);
        voteProductName[1].GetComponent<UnityEngine.UI.Text>().text = Itemtable.Instance.GetItemName(product[1]);
        voteProductName[2].GetComponent<UnityEngine.UI.Text>().text = Itemtable.Instance.GetItemName(product[2]);
    }

    //화면 우측에 표시되는 투표지에서 몇표가 선택되었는지 표시하는 텍스트 갱신
    void UpdateVoteCount()
    {
        voteResultText[0].GetComponent<UnityEngine.UI.Text>().text = "#1번 " + vote[0] + "표";
        voteResultText[1].GetComponent<UnityEngine.UI.Text>().text = "#2번 " + vote[1] + "표";
        voteResultText[2].GetComponent<UnityEngine.UI.Text>().text = "#3번 " + vote[2] + "표";
    }

    //투표시간 텍스트만 갱신, 시간계산은 TwitchUpdate()코루틴에 있음
    void UpdateVoteTimer(int time)
    {
        timerText.GetComponent<UnityEngine.UI.Text>().text = "투표 종료까지 " + time + "초";
    }

    //투표종료 후 우측에 텍스트를 표시하지 않도록 텍스트 내용자체를 비워버림, SetActive true/false하는방식도 있지만 우선은 이 방식을 사용
    public void DeleteVoteTextAll()
    {
        for (int loop = 0; loop < 3; ++loop)
        {
            voteProductName[loop].GetComponent<UnityEngine.UI.Text>().text = "";
            voteResultText[loop].GetComponent<UnityEngine.UI.Text>().text = "";
        }
        timerText.GetComponent<UnityEngine.UI.Text>().text = "";
    }

    //트위치가 연결되어 있는지 확인하는 함수, 외부에서도 쉽게 부르기 위해 사용
    public bool IsTwitchConnected()
    {
        if (twitchClient == null)
        {
            return false;
        }
        else if (twitchClient.Connected == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}//end of Class



