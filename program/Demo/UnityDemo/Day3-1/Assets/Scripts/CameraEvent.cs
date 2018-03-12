using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour {
    Cinemachine.CinemachineVirtualCamera a;

	// Use this for initialization
	void Start () {
        a = GetComponent<Cinemachine.CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            a.m_Lens.FieldOfView = 20;
        }
	}
}
