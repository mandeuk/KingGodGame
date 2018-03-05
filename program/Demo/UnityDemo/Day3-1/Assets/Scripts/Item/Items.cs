using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public class Item
    {
        int itemNum;
        Vector3 itemPos;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Effect()
    {
        PlayerAttack.normalDamage += 10;
        gameObject.SetActive(false);
    }
}
