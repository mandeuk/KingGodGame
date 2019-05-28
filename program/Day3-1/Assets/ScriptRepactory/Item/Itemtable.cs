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
    SoulOfImp       = 40,
    CheatItem       = 99
}

public class Itemtable : MonoBehaviour {
    private static Itemtable instance = null;
    private PlayerBase playerinstance;

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

        playerinstance = PlayerBase.instance;

        //colorRedSpirit     = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //colorOrangeSpirit  = new Color(1.0f, 0.7f, 0.0f, 1.0f);
        //colorYellowSpirit  = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        //colorGreenSpirit   = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        //colorBlueSpirit    = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        //colorWhiteSpirit   = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //colorBlackSpirit   = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        //colorVioletSpirit  = new Color(0.9f, 0.5f, 0.9f, 1.0f);
        //colorRainbowSpirit = new Color(0.0f, 1.0f, 1.0f, 1.0f);
    }  

    /*
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
    */

    public void ApplyItem(int itemType)
    {
        switch(itemType)
        {
            case 0: // 최대 체력 증가
                MaxHPPlus();
                break;
            case 1: // 한대 침
                PlayerHit();
                break;
            case 2: // 체력 3 회복
                Heal();
                break;
            case 3: // 공격력,공격속도증가
                GreenSpiritApply();
                break;
            case 4: // 이동속도 증가
                BlueSpiritApply();
                break;
            case 5: // 꽝
                WhiteSpiritApply();
                break;
            case 6: // 이동 속도 감소
                VioletSpiritApply();
                break;
            case 7: // 폭주 게이지 증가
                BlackSpiritApply();
                break;
            case 8: // 종합 능력치 증가
                RainbowSpiritApply();
                break;
        }
    }

    void MaxHPPlus()
    {
        GetMaxHP(1.0f);
    }

    void PlayerHit()
    {
        //PlayerBase.instance.SetStatus(30f, true, PlayerBase.instance.AttackPower);
        //PlayerBase.instance.SetStatus(1, false, PlayerBase.instance.MaxHP);
        PlayerBase.instance.Damaged(new DamageNode(2, new GameObject(), 0.1f, 3, 1));
    }

    void Heal()
    {
        //playerinstance.attackSpeed += 1.0f;
        //PlayerBase.instance.SetStatus(0.5f, true, PlayerBase.instance.AttackSpeed);
        PlayerBase.instance.SetStatus(3f, true, PlayerBase.instance.CurHP);
    }

    void GreenSpiritApply()
    {
        //playerinstance.attackPower += 0.5f;
        //playerinstance.attackSpeed += 0.5f;
        PlayerBase.instance.SetStatus(20f, true, PlayerBase.instance.AttackPower);
        PlayerBase.instance.SetStatus(0.5f, true, PlayerBase.instance.AttackSpeed);
    }

    void BlueSpiritApply()
    {
        //playerinstance.moveSpeed += 0.5f;
        PlayerBase.instance.SetStatus(2f, true, PlayerBase.instance.MoveSpeed);
    }

    void WhiteSpiritApply()
    {

    }

    void BlackSpiritApply()
    {
        //playerinstance.devilGage += 6.0f;
        PlayerBase.instance.SetStatus(30f, true, PlayerBase.instance.DevilGage);
    }

    void VioletSpiritApply()
    {
        //playerinstance.attackRange += 0.1f;
        //PlayerBase.instance.SetStatus(0.1f, true, PlayerBase.instance.AttackRange);
        PlayerBase.instance.SetStatus(2f, false, PlayerBase.instance.MoveSpeed);
    }

    void RainbowSpiritApply()
    {
        GetMaxHP(1.0f);
        //playerinstance.attackPower += 1.0f;
        //playerinstance.attackSpeed += 1.0f;
        //playerinstance.moveSpeed += 0.5f;
        //playerinstance.attackRange += 0.1f;
        PlayerBase.instance.SetStatus(30f, true, PlayerBase.instance.AttackPower);
        PlayerBase.instance.SetStatus(0.5f, true, PlayerBase.instance.AttackSpeed);
        PlayerBase.instance.SetStatus(1f, true, PlayerBase.instance.MoveSpeed);
        //PlayerBase.instance.SetStatus(0.1f, true, PlayerBase.instance.AttackRange);
    }


    void GetMaxHP(float getHP)
    {
        PlayerBase.instance.SetStatus(getHP, true, PlayerBase.instance.MaxHP);
        //PlayerBase.instance.SetStatus(getHP, true, PlayerBase.instance.CurHP);
        PlaySceneUIManager.instance.UpdateHPUI();
    }

    public String GetItemName(int itemtype)
    {
        switch(itemtype)
        {
            case 0: return "최대체력 증가";
            case 1: return "한대 침";
            case 2: return "체력 3 회복";
            case 3: return "공격력/공격속도 증가";
            case 4: return "이동속도 증가";
            case 5: return "꽝";
            case 6: return "이동 속도 감소";
            case 7: return "폭주게이지 증가";
            case 8: return "종합능력치 세트";
        }
        return "ERROR";
    }

    // 아이템들 스폰 하는것만으로도 적용되게 해놨음.

    public void SpawnLotusOfAbyss()
    {
        Instantiate(Resources.Load("Prefabs/Item/LotusOfAbyss/LotusOfAbyssObj"), PlayerBase.instance.transform.transform);
    }

    public void SpawnSmallSword()
    {
        Instantiate(Resources.Load("Prefabs/Item/SmallSword/SmallSwordObj"));
    }

    public void SpawnSoulOfImp()
    {
        Instantiate(Resources.Load("Prefabs/Item/SoulOfImp/SoulOfImpObj"), PlayerBase.instance.transform.transform);
    }
}
