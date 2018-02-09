using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Load : MonoBehaviour {

    public TextAsset imageTextAsset;    

    // Use this for initialization
    void Start () {
        var fileName = "MyFile.txt";

        var sr = File.CreateText(fileName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
