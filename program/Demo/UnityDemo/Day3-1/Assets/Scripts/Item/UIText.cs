using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

    Text powertext;

	// Use this for initialization
	void Awake () {
        powertext = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        powertext.text = "Pow" + PlayerStatus.instance.attackPower;
	}
}
