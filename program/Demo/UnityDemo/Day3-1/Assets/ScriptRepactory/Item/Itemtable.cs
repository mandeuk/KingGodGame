using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum ItemNum
{
    Empty           = 0,
    RedSpirit       = 1,
    OrangeSpirit    = 2,
    YellowSpirit    = 3,
    GreenSpirit     = 4,
    BlueSpirit      = 5,
    WhiteSpirit     = 6,
    BlackSpirit     = 7,
    VioletSpirit    = 8,
    RainbowSpirit   = 9,
    LutusOfAbyss    = 25,
    SmallSword      = 39,
    SoulOfImp       = 40
}

public class Itemtable : MonoBehaviour {
    private static Itemtable instance = null;
    private Transform player;

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

        player = PlayerBase.instance.transform;

        colorRedSpirit     = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        colorOrangeSpirit  = new Color(1.0f, 0.7f, 0.0f, 1.0f);
        colorYellowSpirit  = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        colorGreenSpirit   = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        colorBlueSpirit    = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        colorWhiteSpirit   = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //colorBlackSpirit   = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        colorVioletSpirit  = new Color(0.9f, 0.5f, 0.9f, 1.0f);
        //colorRainbowSpirit = new Color(0.0f, 1.0f, 1.0f, 1.0f);
    }  

    public Color SetItemColor(int itemType)
    {
        Color itemColor = colorRedSpirit;//default값
        
        
        switch(itemType)
        {
            case 0:
                itemColor = colorRedSpirit;
                break;
            case 1:
                itemColor = colorOrangeSpirit;
                break;
            case 2:
                itemColor = colorYellowSpirit;
                break;
            case 3:
                itemColor = colorGreenSpirit;
                break;
            case 4:
                itemColor = colorBlueSpirit;
                break;
            case 5:
                itemColor = colorWhiteSpirit;
                break;
            //case 7:
            //    itemColor = colorBlackSpirit;
            //    break;
            case 6:
                itemColor = colorVioletSpirit;
                break;
            //case 8:
            //    itemColor = colorRainbowSpirit;
            //    break;
        }

        return itemColor;
    }

    public void ApplyItem(int itemType)
    {
        switch(itemType)
        {
            case 0:
                RedSpiritApply();
                break;
            case 1:
                OrangeSpiritApply();
                break;
            case 2:
                YellowSpiritApply();
                break;
            case 3:
                GreenSpiritApply();
                break;
            case 4:
                BlueSpiritApply();
                break;
            case 5:
                WhiteSpiritApply();
                break;
            //case 7:
            //    BlackSpiritApply();
            //    break;
            case 6:
                VioletSpiritApply();
                break;
            //case 8:
            //    RainbowSpiritApply();
            //    break;
        }
    }

    void RedSpiritApply()
    {
        GetMaxHP(1.0f);
        PlaySceneUIManager.instance.UpdateHPUI();
    }

    void OrangeSpiritApply()
    {
        PlayerBase.instance.attackPower += 1.0f;
    }

    void YellowSpiritApply()
    {
        PlayerBase.instance.attackSpeed += 1.0f;
    }

    void GreenSpiritApply()
    {
        PlayerBase.instance.attackPower += 0.5f;
        PlayerBase.instance.attackSpeed += 0.5f;
    }

    void BlueSpiritApply()
    {
        PlayerBase.instance.moveSpeed += 0.5f;
    }

    void WhiteSpiritApply()
    {
    }

    void BlackSpiritApply()
    {
        PlayerBase.instance.devilGage += 6.0f;
    }

    void VioletSpiritApply()
    {
        PlayerBase.instance.attackRange += 0.1f;
    }

    void RainbowSpiritApply()
    {
        GetMaxHP(1.0f);
        PlayerBase.instance.attackPower += 1.0f;
        PlayerBase.instance.attackSpeed += 1.0f;
        PlayerBase.instance.moveSpeed += 0.5f;
        PlayerBase.instance.attackRange += 0.1f;
    }


    void GetMaxHP(float getHP)
    {
        PlayerBase.instance.maxHP += getHP;
        if (PlayerBase.instance.maxHP > 10)
            PlayerBase.instance.maxHP = 10;
        PlayerBase.instance.curHP += getHP;
        if (PlayerBase.instance.curHP > PlayerBase.instance.maxHP)
            PlayerBase.instance.curHP = PlayerBase.instance.maxHP;
    }

    public String GetItemName(int itemtype)
    {
        switch(itemtype)
        {
            case 0: return "RedSpirit";
            case 1: return "OrangeSpirit";
            case 2: return "YellowSpirit";
            case 3: return "GreenSpirit";
            case 4: return "BlueSpirit";
            case 5: return "WhiteSpirit";
            //case 6: return "BlackSpirit";
            case 6: return "VioletSpirit";
            //case 8: return "RainbowSpirit";
        }

        return "ERROR";
    }

    public void SpawnLotusOfAbyss()
    {
        Instantiate(Resources.Load("Prefabs/Item/LotusOfAbyss/LotusOfAbyssObj"), player.transform);
    }

    public void SpawnSmallSword()
    {
        Instantiate(Resources.Load("Prefabs/Item/SmallSword/SmallSwordObj"));
    }

    public void SpawnSoulOfImp()
    {
        Instantiate(Resources.Load("Prefabs/Item/SoulOfImp/SoulOfImpObj"), player.transform);
    }
}
