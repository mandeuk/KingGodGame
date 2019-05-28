using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public static DialogManager instance = null;

    int curdialog, curpage, maxpage;
    Transform curTextObject;

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

    public bool NextDialog()
    {
        if (curpage < maxpage)
        {
            curpage++;
            UpdateDialog();
            return true;
        }
        else if (curpage >= maxpage)
        {
            return false;
        }
        else
            return false;
    }

    void UpdateDialog()
    {
        switch(curdialog)
        {
            case 1://1스테이지 시작 다이얼로그
                {
                    switch(curpage)
                    {
                        case 1:
                            curTextObject.GetComponent<Text>().text = "여기가 경계의탑...?";
                            break;
                        case 2:
                            curTextObject.GetComponent<Text>().text = "생각보다 기분나쁜 곳이야....\n일단 주위를 조금 둘러볼까...";
                            break;
                        case 3:
                            curTextObject.GetComponent<Text>().text = "(이동은 키보드 WASD로 할수있다.)";
                            break;
                        case 4:
                            curTextObject.GetComponent<Text>().text = "(K키로 EX무브, L키를 짧게 누르면 회피, L키를 길게눌러 차지공격을 할수있다.)";
                            break;
                    }
                }
                break;
        }
    }

    public void StartDialog(int dialognumber, Transform textObject)
    {
        curTextObject = textObject;
        curdialog = dialognumber;
        curpage = 1;

        switch(dialognumber)
        {
            case 1://1스테이지 시작 다이얼로그
                maxpage = 4;
                break;
        }

        UpdateDialog();//다이얼로그 텍스트내용 갱신
    }
    
}
