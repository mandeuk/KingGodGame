using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMoveCam : MonoBehaviour {
    public static EXMoveCam instance = null;
    public GameObject mainCamera;
    public GameObject normalCamera;

    void Awake()
    {
        instance = this;
    }


    public IEnumerator cameraHitEvent(float time)
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        normalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(time);
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        normalCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        Time.timeScale = 1;

        yield break;
    }


    public void playEXCameraEvent(float time)
    {
        StartCoroutine(cameraHitEvent(time));
    }
}
