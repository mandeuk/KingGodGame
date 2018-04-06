using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma strict

public class BornMovement : MonoBehaviour {

    private Transform thisParent;
    private Rigidbody thisRigidbody;
    private Vector3 parentPosLastFrame;

	// Use this for initialization
	void Start () {
        thisParent = transform.parent;

        thisRigidbody = transform.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        thisRigidbody.AddForce((parentPosLastFrame - thisParent.position) * 100);

        parentPosLastFrame = thisParent.position;
    }
}
