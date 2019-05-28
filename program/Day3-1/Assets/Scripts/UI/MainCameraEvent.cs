using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    private void FixedUpdate()
    {
        GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
    }
}
