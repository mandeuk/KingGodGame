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
        onFire = true;
    }

    public virtual void BulletHit()
    {        
        onFire = false;

        Attacker.GetComponent<RangeAttack>().BulletHit(gameObject);
    }

    // 함수 기능 :  5초뒤에 사라지게 함.
    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(5.0f);
        BulletHit();

        yield break;
    }
}
