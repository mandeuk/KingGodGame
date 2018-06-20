using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour {
    public bool onFire;
    public GameObject Attacker;

    // Use this for initialization
    void Awake () {
		
	}

    public virtual void Init()
    {
        onFire = false;        
    }

    public virtual void BulletHit()
    {
        onFire = false;
    }
}
