using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    GameObject ItemMgr;
    int ItemType;

	// Use this for initialization
	void Awake () {
        ItemMgr = GameObject.Find("ItemManager");//ItemManager는 오직 하나이기 때문에 GameObject.Find로 Hierarchy목록에서 찾아서 가져온다.
        ItemType = Random.Range(1, 2);
	}

    public void ItemEffect()
    {
        PlayerAttack.normalDamage += 10;
        ItemMgr.GetComponent<ItemManager>().GetItem(gameObject);//GameObject ItemManager속 ItemManager 스크립트를 불러와 GetItem()함수 호출
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemEffect();
            gameObject.SetActive(false);//충돌한 Item 오브젝트를 비활성화
            //Debug.Log("Compare tag item.cs 충돌!");
        }
    }
}
