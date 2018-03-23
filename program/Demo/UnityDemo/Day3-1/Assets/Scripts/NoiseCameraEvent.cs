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

    public IEnumerator cameraHitEvent()
    {
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 110;

        yield return new WaitForSeconds(0.1f);
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 90;

        yield break;
    }
}
