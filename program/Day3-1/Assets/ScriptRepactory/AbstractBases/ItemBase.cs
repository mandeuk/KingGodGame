using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    protected GameObject player;
    protected ItemNum thisItemNum;

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        player = PlayerBase.instance.gameObject;
    }

    public virtual void ItemApply(ItemNum itemNum)
    {
        GameManager.PlayerItemSave(itemNum);
        //Destroy(this);    디스트로이는 게임오브젝트로 하이어라키에 존재할때만 불려져서 base.로 함수를 부를수가 없음.
        //                  아이템스폰도 각자하는게아니라 위의 itemNum 에서 받아서 할수있게 델리게이트로 작성해야할듯.
    }
}
