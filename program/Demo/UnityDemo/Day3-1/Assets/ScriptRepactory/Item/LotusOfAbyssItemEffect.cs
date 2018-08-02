using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyssItemEffect : ItemBase {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            ItemApply();
        }
    }

    private void OnEnable()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void ItemApply()
    {
        Instantiate(Resources.Load("Prefabs/Item/LotusOfAbyss/LotusOfAbyssObj"), player.transform);
        EffectManager.playGetLegendItemEffet();
        gameObject.SetActive(false);
        Destroy(this);
    }
}
