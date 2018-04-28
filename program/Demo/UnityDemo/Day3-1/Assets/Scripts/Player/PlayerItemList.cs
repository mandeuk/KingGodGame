﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemList : MonoBehaviour {
    public List<GameObject> itemList = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        LotusOfAbyss();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 일단 함수로 두고
    // 원래는 List<Item> ItemList로 해서 Item[i].Apply() 식으로 해야겠음
    // 먹었을때 apply가 적용되던가.
    public void LotusOfAbyss()
    {
        GetComponent<PlayerEffect>().InitBlackWaveEffect();
    }
}
