using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithBullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WraithEffect.instance.BulletHit(this.gameObject);
            other.transform.GetComponent<PlayerHealth>().PlayerDamaged();
        }


        if (other.CompareTag("mapClearCol"))
        {
            WraithEffect.instance.BulletHit(this.gameObject);

        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
