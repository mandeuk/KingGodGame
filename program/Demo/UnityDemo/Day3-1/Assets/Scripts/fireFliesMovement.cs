using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFliesMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.Translate(new Vector3(Random.Range(-20, 20), Random.Range(-2, 2), Random.Range(-20, 20)) * 0.1f * Time.deltaTime);
    }
}
