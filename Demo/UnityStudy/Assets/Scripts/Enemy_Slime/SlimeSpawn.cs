﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawn : MonoBehaviour {

    public GameObject slimeChar;
    private List<GameObject> slimeCharList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            float randomPosX = Random.Range(-10f, 10f);
            float randomPosZ = Random.Range(-10f, 10f);
            GameObject slimeClone = Instantiate(slimeChar, new Vector3(randomPosX, 0f, randomPosZ), transform.rotation);
            slimeCharList.Add(slimeClone);
            slimeClone.SetActive(true);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
