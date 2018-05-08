using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour {
    public List<Image> HPiconList = new List<Image>();
    public List<Image> usedHPiconList = new List<Image>();
    public Image image;
    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        image.overrideSprite = Resources.Load<Sprite>("Image/Sprite/twitchlogo");
        HPiconList.Add(image);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
