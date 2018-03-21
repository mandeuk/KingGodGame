using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMovePosMovement : MonoBehaviour {
    Vector3 startPos;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            print(Vector3.Distance(transform.position, other.transform.position));
            //transform.position -= transform.forward;

        }
    }


    // Use this for initialization
    void Start () {
        startPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
