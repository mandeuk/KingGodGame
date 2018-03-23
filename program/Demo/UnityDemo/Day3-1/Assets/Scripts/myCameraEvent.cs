using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCameraEvent : MonoBehaviour {
    Cinemachine.CinemachineVirtualCamera mainCamera;
    public GameObject raphael;

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update () {
        if(raphael.GetComponent<PlayerAttack>().enemyInList)
            mainCamera.m_Lens.FieldOfView = raphael.GetComponent<Animator>().GetFloat("cameraFOV") + 40;
        else
            mainCamera.m_Lens.FieldOfView = 40;
    }
}
