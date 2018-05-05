using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemList : MonoBehaviour {
    public List<GameObject> itemList = new List<GameObject>();
    public GameObject auraEffect;

	// Use this for initialization
	void Awake () {
        //LotusOfAbyss(); // 임의로 발동되게 함.
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            LotusOfAbyss();
        }
	}

    // 일단 함수로 두고
    // 원래는 List<Item> ItemList로 해서 Item[i].Apply() 식으로 해야겠음
    // 먹었을때 apply가 적용되던가.
    public void LotusOfAbyss()
    {
        GetComponent<PlayerEffect>().ChangeSlashEffectBlackWave();
        GetComponent<PlayerEffect>().InitBlackWaveEffect();
        GetComponent<PlayerAttack>().lotusOn = true;
        auraEffect.SetActive(true);
    }
}
