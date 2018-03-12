using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour {
    public MatContainer[] matCon = new MatContainer[5];

	// Use this for initialization
	void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach(MatContainer a in matCon)
                a.changeAfterImage();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (MatContainer a in matCon)
                a.changeraphaelMat();
        }
    }
}
