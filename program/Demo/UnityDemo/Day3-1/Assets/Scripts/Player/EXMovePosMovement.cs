using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMovePosMovement : MonoBehaviour {
    Vector3 startPos;

    private void OnTriggerStay(Collider other)
    {

    }


    // Use this for initialization
    void Start () {
        startPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
