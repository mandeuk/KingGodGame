using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMoveCam : MonoBehaviour {
    public GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainVirtualCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator cameraHitEvent(float time)
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(time);
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1500;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        Time.timeScale = 1;

        yield break;
    }
    public void CameraCamTurning()
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1200;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
    }
}
