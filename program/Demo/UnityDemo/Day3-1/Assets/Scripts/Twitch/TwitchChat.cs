using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.ComponentModel;
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
    public int[] vote = { 0, 0, 0, 0};//투표 횟수를 저장하기 위한 배열 선언
    SortedDictionary<string, string> IDList = new SortedDictionary<string, string>();//SortedDictionary를 사용해 투표한 사용자의 ID를 기록하고 이를 통해 중복체크기능 수행



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
        
        Connect();//트위치 irc서버에 접속시도
    }//end of Start()

    private void Start()
    {
        votestart = false;
    }

    void Update()
    {
        //if (!twitchClient.Connected)//만약 TcpClient가 irc서버에 접속하지 못했을 경우 다시 접속 시도
        //{
        //    Connect();//트위치 irc서버에 접속시도
        //}
        //else
        //    ReadChat();//만약 irc서버에 접속을 성공했다면 채팅을 읽어들인다.
    }//end of Update()



    public IEnumerator TwitchUpdate()
    {
        float timer = 0;
        print("트위치업데이트start");
        while(votestart)
        {
            timer += Time.deltaTime;

            if (!twitchClient.Connected)//만약 TcpClient가 irc서버에 접속하지 못했을 경우 다시 접속 시도
            {
                Connect();//트위치 irc서버에 접속시도
            }
            else
                ReadChat();//만약 irc서버에 접속을 성공했다면 채팅을 읽어들인다.


            if (timer >= 10.0f)
            {
                StopVote();
                print("시간이 만료되어 투표종료");
            }

            yield return null;
        }
        print("트위치업데이트end");
        StartVote();//투표가 멈추지 않고 계속되도록
        yield break;
    }


    
    // 함수이름 : Connect()
    // 함수기능 : TcpClient로 트위치 IRC서버에 연결한 후 StreamReader와 StreamWriter를 설정하고 자신의 채널에 접속한다.
    public void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667); //트위치의 IRC 채팅서버 주소와 포트번호, IRC서버는 보통 6667번 포트를 많이 사용한다.
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.WriteLine("PRIVMSG #" + channelName + " :Counterflow와 Twitch채팅서버가 성공적으로 연결되었습니다.");
        writer.Flush();//Flush() = 해당 writer의 버퍼를 clear하는 기능

    }//end of Connect()


    
    // 함수이름 : ReadChat()
    // 함수기능 : Connect()에서 설정한 NetworkStream을 사용해 IRC서버의 채팅내용을 실시간으로 불러들인다.
    //            현재 채팅내역은 유니티 콘솔창에 출력하도록 되어있다. 추후 수정할 예정
    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine();

            print(message);//콘솔출력
            //////////////////////////////////////////////////////////
            //  트위치 채팅채널에서 채팅을 입력할 경우
            //  :mandeuk!mandeuk@mandeuk.tmi.twitch.tv PRIVMSG #mandeuk :test1
            //  이와 같은문구가 전송되고 여기서 채팅을 한 사람과 채팅 내역을 뽑아내기 위해
            //  앞쪽의 닉네임 문자열을 뽑아내기 위해 IndexOf()함수로 !의 위치를 리턴, splitPoint에 저장
            //  Substring()함수를 사용해 :mandeuk! 특수문자 사이의 mandeuk을 뽑아낸다.
            ///////////////////////////////////////////////////////////
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
                    IDList.Add(chatName, chatName);
                    switch (message)
                    {
                        case "#1":
                            vote[1] += 1;
                            print("1번 투표수" + vote[1]);
                            break;
                        case "#2":
                            vote[2] += 1;
                            print("2번 투표수" + vote[2]);
                            break;
                        case "#3":
                            vote[3] += 1;
                            print("3번 투표수" + vote[3]);
                            break;
                    }
                }
            }//end of if
        }//end of if
    }// end of ReadChat()



    public void StartVote()
    {
        votestart = true;
        System.Array.Clear(vote, 0, 4);
        IDList.Clear();
        StartCoroutine(TwitchUpdate());
        //print("코루틴 시작");
        SendChatMessage("투표시작");
    }

    public void StopVote()
    {
        //print("StopVote() 투표종료");
        votestart = false;
        ProcessVote();
    }

    public void ProcessVote()
    {
        //print("ProcessVote() 투표결과도출");
        int selected = 0;
        bool randomselect = false;

        if(vote[1] > vote[2])
        {
            if(vote[1] > vote[3]) {
                selected = 1;
            }
            else if (vote[1] == vote[3]) {
                selected = UnityEngine.Random.Range(1, 2);
                if (selected == 2)
                    selected = 3;
                randomselect = true;
            }
            else if (vote[1] < vote[3]) {
                selected = 3;
            }
        }


        else if (vote[1] == vote[2]) {
            if (vote[1] > vote[3]) {
                selected = UnityEngine.Random.Range(1, 2);
                randomselect = true;
            }
            else if (vote[1] == vote[3]) {
                selected = UnityEngine.Random.Range(1, 3);
                randomselect = true;
            }
            else if (vote[1] < vote[3]) {
                selected = 3;
            }
        }


        else if (vote[1] < vote[2]) {
            if (vote[2] > vote[3])
            {
                selected = 2;
            }
            else if (vote[2] == vote[3])
            {
                selected = UnityEngine.Random.Range(2, 3);
                randomselect = true;
            }
            else if (vote[2] < vote[3])
            {
                selected = 3;
            }
        }

        if (randomselect)
        {
            SendChatMessage("투표가 종료되었습니다. 투표수가 같은 선택지가 존재하여, 그 중 무작위로 " + selected + "번이 선택되었습니다.");
        }
        else
        {
            SendChatMessage("투표가 종료되었습니다." + selected + "번이 선택되었습니다.");
        }
    }//end of ProcessVote()



    void SendChatMessage(string msg)
    {
        //writer = new StreamWriter(twitchClient.GetStream());
        
        writer.WriteLine("PRIVMSG #" + channelName + " :" + msg);

        writer.Flush();
    }
}//end of Class



