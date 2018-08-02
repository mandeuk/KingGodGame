using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerItemList : MonoBehaviour {
    public List<GameObject> itemList = new List<GameObject>();

    // 함수 기능 :
    private void Awake()
    {
        InsertItem();
    }

    public static void InsertItem()
    {

    }
}
