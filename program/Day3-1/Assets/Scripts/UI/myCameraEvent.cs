using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCameraEvent : MonoBehaviour {
    Cinemachine.CinemachineVirtualCamera mainCamera;
    GameObject raphael;

	// Use this for initialization
	void Start () {
        raphael = PlayerBase.instance.gameObject;
        mainCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update () {
        mainCamera.m_Lens.FieldOfView = raphael.GetComponent<Animator>().GetFloat("cameraFOV")/2 + 40;
    }
}
