using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifebarManager : MonoBehaviour {
    public static BossLifebarManager instance = null;

    Image gauge;
    GameObject bossObject;

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

        gauge = this.transform.GetChild(1).gameObject.GetComponent<Image>();
    }

    //보스체력 게이지 갱신, 체력변화가 있을때 호출해주세요
    public void UpdateBosslifeGauge(float maxHP, float curHP)
    {
        gauge.fillAmount = Mathf.Lerp(0.0f, 1.0f, (curHP / maxHP));
    }

    public void BosslifeGaugeFull()
    {
        gauge.fillAmount = Mathf.Lerp(0.0f, 1.0f, 1);
    }

    //보스가 생성될 때 체력UI를 띄워주는함수
    public void ActivateBosslifeGauge()
    {
        BosslifeGaugeFull();
        this.transform.gameObject.SetActive(true);
    }

    //보스 사망시 UI 없애주는 함수
    public void InActivateBosslifeGauge()
    {
        this.transform.gameObject.SetActive(false);
    }
}
