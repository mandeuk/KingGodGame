using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncroachmentManager : MonoBehaviour {

    public static EncroachmentManager instance = null;

    Image gauge;
    Sprite normalbar, alertbar;
    GameObject bluepoint, redpoint;

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

        //프리팹 내부에 포함되어있는 게임오브젝트 저장해두기(컨트롤 해야할 것들)
        gauge = this.transform.GetChild(2).gameObject.GetComponent<Image>();
        bluepoint = this.transform.GetChild(3).gameObject;
        redpoint = this.transform.GetChild(4).gameObject;

        //게이지 색이 변할때를 대비해 미리 이미지 불러놓기
        normalbar = Resources.Load<Sprite>("Image/UI/encroachment_gauge_progressbar");
        alertbar = Resources.Load<Sprite>("Image/UI/encroachment_gauge_alertbar");

        //붉은빛 포인트 처음 위치 설정
        RedpointInitpos();
    }

    //외부에서 데빌 게이지 수치가 변했을 때 호출하면 UI에서 변한 수치에 맞게 갱신해주는 함수
    public void UpdateDevilGauge()
    {
        gauge.fillAmount = Mathf.Lerp(0.0f, 1.0f, (PlayerBase.instance.devilGage / 100.0f));
        
        if (PlayerBase.instance.devilGage < 80)//만약 폭주게이지가 80 미만이라면
        {
            if (bluepoint.active == true)//파란점이 켜져있으면(계속 폭주게이지가 낮은 상태)
            {
                
            }
            else//파란점이 꺼져있으면(폭주게이지가 80을 넘어섰다가 다시 줄어든 경우)
            {
                bluepoint.SetActive(true);
                gauge.sprite = normalbar;//폭주게이지를 파란색으로
            }
            BluepointUpdate();
        }
        else//만약 폭주게이지가 80 이상이라면
        {
            if(bluepoint.active == true)//파란점이 켜져있으면(이제 막 폭주게이지가 80을 넘어서는순간)
            {
                bluepoint.SetActive(false);
                gauge.sprite = alertbar;//폭주게이지를 보라색으로
            }
            else//파란점이 꺼져있으면(폭주게이지가 80을 게속 넘어서있던 상태)
            {
                
            }
            RedpointUpdate();
        }
    }// end of public void UpdateDevilGauge()

    void BluepointUpdate()
    {
        int windowwidth = Screen.width;//화면 가로 해상도

        //현재 폭주게이지의 끝에 빛나는이미지의 좌표값 계산
        Vector3 brightpointpos;
        brightpointpos.x = windowwidth * gauge.fillAmount;
        brightpointpos.y = 0.0f;
        brightpointpos.z = bluepoint.transform.position.z;

        bluepoint.transform.SetPositionAndRotation(brightpointpos, new Quaternion());
    }

    void RedpointUpdate()
    {
        int windowwidth = Screen.width;//화면 가로 해상도

        //현재 폭주게이지의 끝에 빛나는이미지의 좌표값 계산
        Vector3 brightpointpos;
        brightpointpos.x = windowwidth * gauge.fillAmount;
        brightpointpos.y = 0.0f;
        brightpointpos.z = redpoint.transform.position.z;

        redpoint.transform.SetPositionAndRotation(brightpointpos, new Quaternion());
    }

    void RedpointInitpos()
    {
        int windowwidth = Screen.width;//화면 가로 해상도

        //현재 폭주게이지의 끝에 빛나는이미지의 좌표값 계산
        Vector3 brightpointpos;
        brightpointpos.x = windowwidth * 0.8f;
        brightpointpos.y = 0.0f;
        brightpointpos.z = redpoint.transform.position.z;

        redpoint.transform.SetPositionAndRotation(brightpointpos, new Quaternion());
    }
}
