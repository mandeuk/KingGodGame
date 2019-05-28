using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    Rigidbody rig;
	// Use this for initialization
	void Awake () {
        rig = GetComponent<Rigidbody>();
	}

    private void Start()
    {
        rig.AddForce(transform.forward * 200);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
