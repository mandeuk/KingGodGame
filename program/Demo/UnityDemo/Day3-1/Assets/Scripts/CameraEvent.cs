using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour {
    Cinemachine.CinemachineVirtualCamera a;


	// Use this for initialization
	void Start () {
        a = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        //StartCoroutine(cameraMove());
	}
	
	// Update is called once per frame
	void Update () {
        
        
    }
    
    public IEnumerator cameraMove()
    {
        while (true)
        {
            //a.m_Lens.FieldOfView = 20;
            yield return new WaitForSeconds(1.0f);
        }

        //yield break;
    }
}
