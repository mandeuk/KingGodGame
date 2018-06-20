using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadObjMovement : MonoBehaviour {
    Animator anim;

	// Use this for initialization
	void Awake () {
        anim = PlayerBase.instance.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(0, transform.position.y + anim.GetFloat("PlayerYPos"), 0);     
        transform.localPosition = new Vector3(0,anim.GetFloat("PlayerYPos"), 0.4f);

    }
}
