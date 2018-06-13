using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemList : MonoBehaviour {

    public List<GameObject> itemList = new List<GameObject>();
    public GameObject auraEffect;
    public GameObject LotusGetEffect;


    // 일단 함수로 두고
    // 원래는 List<Item> ItemList로 해서 Item[i].Apply() 식으로 해야겠음
    // 먹었을때 apply가 적용되던가.
    public void LotusOfAbyss()
    {
        auraEffect.SetActive(true);
        LotusGetEffect.SetActive(true);
        LotusGetEffect.GetComponent<ParticleSystem>().Play();
    }
}
