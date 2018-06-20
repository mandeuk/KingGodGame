using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCameraEvent : MonoBehaviour {
    public static NoiseCameraEvent instance = null;
    public GameObject mainCamera;
    public GameObject exMoveCamera;

	// Use this for initialization
	void Awake () {
        instance = this;
    }

    public IEnumerator cameraHitEvent(float time)
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        exMoveCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(time);

        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        exMoveCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        Time.timeScale = 1;

        yield break;
    }

    public void playNormalCameraEvent(float time)
    {
        StartCoroutine(cameraHitEvent(time));
    }
}
