using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatItemEffect : ItemBase {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            ItemApply(thisItemNum);
        }
    }

    private void OnEnable()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        thisItemNum = ItemNum.CheatItem;
    }

    public override void ItemApply(ItemNum itemNum)
    {
        base.ItemApply(itemNum);
        PlayerBase.instance.RealCheat();
        EffectManager.playGetRareItemEffet();
        gameObject.SetActive(false);
        Destroy(this);
    }
}
