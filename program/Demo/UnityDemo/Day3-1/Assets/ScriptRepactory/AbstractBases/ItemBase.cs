using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour {
    protected GameObject player;

    private void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        player = PlayerBase.instance.gameObject;
    }

    public virtual void ItemApply()
    {
        Destroy(this);
    }
}
