using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBullet : MonoBehaviour {
    public bool onFire;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerHealth>().PlayerDamaged(this.gameObject);
            WraithEffect.instance.BulletHit(this.gameObject);
            onFire = false;
        }


        if (other.CompareTag("mapClearCol"))
        {
            WraithEffect.instance.BulletHit(this.gameObject);
            onFire = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
        onFire = true;
    }
    

    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(5.0f);
        WraithEffect.instance.BulletHit(this.gameObject);
        onFire = false;

        yield break;
    }
}
