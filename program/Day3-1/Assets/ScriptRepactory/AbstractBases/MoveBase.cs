using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour {    
    protected ObjectBase entity;
    protected Rigidbody rigid;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        entity = GetComponent<ObjectBase>();
        rigid = GetComponent<Rigidbody>();
    }
}
