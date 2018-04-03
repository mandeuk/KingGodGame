using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseCameraEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator cameraHitEvent(float time)
    {
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 110;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(time);
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 90;
        Time.timeScale = 1;

        yield break;
    }
}
