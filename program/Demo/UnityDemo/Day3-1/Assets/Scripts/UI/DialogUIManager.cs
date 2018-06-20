using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUIManager : MonoBehaviour {

    GameObject playerCharacterImg, dialogTextboxBg;



    private void Awake()
    {
        //dialogUI = Instantiate(Resources.Load("Prefabs/UI/Dialog/DialogUI"), PlaySceneUIManager.instance.canvas.transform) as GameObject;
        gameObject.SetActive(false);
        playerCharacterImg = GameObject.Find("LeftCharacterImg");
        dialogTextboxBg = GameObject.Find("DialogTextbox");
    }

	// Use this for initialization
	void Start () {
        
    }
	
    

}
