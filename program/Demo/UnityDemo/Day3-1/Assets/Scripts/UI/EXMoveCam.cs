using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMoveCam : MonoBehaviour {
    public static EXMoveCam instance = null;
    public GameObject mainCamera;
    public GameObject normalCamera;
    PlayerBase player;

    void Awake()
    {
        instance = this;
        player = PlayerBase.instance;
    }


    public IEnumerator cameraHitEvent(float time)
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        normalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;

        //player.PlayerStiff();
        yield return new WaitForSecondsRealtime(0.3f);

        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        normalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;


        yield break;
    }

    public void playEXCameraEvent(float time)
    {
        StartCoroutine(cameraHitEvent(time));
    }
}
