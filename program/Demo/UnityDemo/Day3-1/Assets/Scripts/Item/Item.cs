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



    // 함수이름 : void ItemEffect()
    // 기능 : 획득한 아이템의 효과를 적용하는 함수, ItemManager와 PlayerStatus에 접근합니다.
    private void ItemEffect()
    {
        //PlayerAttack.normalDamage += 10;
        PlayerStatus.instance.attackPower += 10;
        ItemManager.Instance.GetItem(gameObject);
    }



    // 함수이름 : void OnTriggerEnter(Collider other)
    // 기능 : 아이템과 충돌한 물체가 Player일 경우 아이템을 획득하도록 하는 기능을 가지고 있습니다
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
