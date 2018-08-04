using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyssItemEffect : ItemBase {

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
        thisItemNum = ItemNum.LutusOfAbyss;
    }

    public override void ItemApply(ItemNum itemNum)
    {
        base.ItemApply(itemNum);
        Itemtable.Instance.SpawnLotusOfAbyss();
        EffectManager.playGetLegendItemEffet();
        gameObject.SetActive(false);
        Destroy(this);
    }
}
