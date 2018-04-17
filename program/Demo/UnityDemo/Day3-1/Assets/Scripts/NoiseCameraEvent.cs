using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCameraEvent : MonoBehaviour {
    public GameObject mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindWithTag("MainVirtualCamera");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator cameraHitEvent(float time)
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(time);
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        Time.timeScale = 1;
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;

        yield break;
    }
}
