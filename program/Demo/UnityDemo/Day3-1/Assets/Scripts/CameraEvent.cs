using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour {
    Cinemachine.CinemachineVirtualCamera mainCamera;
    public GameObject raphael;

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
	}
	
	// Update is called once per frame
	void Update () {
        mainCamera.m_Lens.FieldOfView = raphael.GetComponent<Animator>().GetFloat("cameraFOV") + 40;
    }
    
    public IEnumerator cameraMove()
    {
        while (true)
        {
            mainCamera.m_Lens.FieldOfView = raphael.GetComponent<Animator>().GetFloat("cameraFOV");
            yield return new WaitForSeconds(1.0f);
        }
    }
}
