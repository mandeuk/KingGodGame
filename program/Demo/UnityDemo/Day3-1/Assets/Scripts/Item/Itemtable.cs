using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemtable : MonoBehaviour {
    private static Itemtable instance = null;

    Color colorRedSpirit;
    Color colorOrangeSpirit;
    Color colorYellowSpirit;
    Color colorGreenSpirit;
    Color colorBlueSpirit;
    Color colorWhiteSpirit;
    Color colorBlackSpirit;
    Color colorVioletSpirit;
    Color colorRainbowSpirit;

    public static Itemtable Instance
    {
        get
        {
            return instance;
        }
    }

    // Use this for initialization
    void Awake () {
        //아이템매니저를 싱글턴 오브젝트로 만들어 씬이 변경되어도 삭제되지 않고 살아서 아이템을 관리하도록 해줍니다.
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

        colorRedSpirit     = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        colorOrangeSpirit  = new Color(1.0f, 0.7f, 0.0f, 1.0f);
        colorYellowSpirit  = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        colorGreenSpirit   = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        colorBlueSpirit    = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        colorWhiteSpirit   = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        colorBlackSpirit   = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        colorVioletSpirit  = new Color(0.9f, 0.5f, 0.9f, 1.0f);
        colorRainbowSpirit = new Color(0.0f, 1.0f, 1.0f, 1.0f);
    }
   

    // Update is called once per frame
    void Update () {
		
	}

    public Color SetItemColor(int itemType)
    {
        Color itemColor = colorRedSpirit;//default값

        switch(itemType)
        {
            case 1:
                itemColor = colorRedSpirit;
                break;
            case 2:
                itemColor = colorOrangeSpirit;
                break;
            case 3:
                itemColor = colorYellowSpirit;
                break;
            case 4:
                itemColor = colorGreenSpirit;
                break;
            case 5:
                itemColor = colorBlueSpirit;
                break;
            case 6:
                itemColor = colorWhiteSpirit;
                break;
            case 7:
                itemColor = colorBlackSpirit;
                break;
            case 8:
                itemColor = colorVioletSpirit;
                break;
            case 9:
                itemColor = colorRainbowSpirit;
                break;
        }

        return itemColor;
    }

    public void ApplyItem(int itemType)
    {
        switch(itemType)
        {
            case 1:
                RedSpiritApply();
                break;
            case 2:
                OrangeSpiritApply();
                break;
            case 3:
                YellowSpiritApply();
                break;
            case 4:
                GreenSpiritApply();
                break;
            case 5:
                BlueSpiritApply();
                break;
            case 6:
                WhiteSpiritApply();
                break;
            case 7:
                BlackSpiritApply();
                break;
            case 8:
                VioletSpiritApply();
                break;
            case 9:
                RainbowSpiritApply();
                break;
        }
    }

    void RedSpiritApply()
    {
        PlayerStatus.instance.healthPoint += 0.5f;
    }

    void OrangeSpiritApply()
    {
        PlayerStatus.instance.attackPower += 1.0f;
    }

    void YellowSpiritApply()
    {
        PlayerStatus.instance.attackSpeed += 1.0f;
    }

    void GreenSpiritApply()
    {
        PlayerStatus.instance.attackPower += 0.5f;
        PlayerStatus.instance.attackSpeed += 0.5f;
    }

    void BlueSpiritApply()
    {
        PlayerStatus.instance.moveSpeed += 0.5f;
    }

    void WhiteSpiritApply()
    {
    }

    void BlackSpiritApply()
    {
        PlayerStatus.instance.devilGage += 6.0f;
    }

    void VioletSpiritApply()
    {
        PlayerStatus.instance.attackRange += 0.1f;
    }

    void RainbowSpiritApply()
    {
        PlayerStatus.instance.healthPoint += 0.5f;
        PlayerStatus.instance.attackPower += 1.0f;
        PlayerStatus.instance.attackSpeed += 1.0f;
        PlayerStatus.instance.moveSpeed += 0.5f;
        PlayerStatus.instance.attackRange += 0.1f;
    }
}
