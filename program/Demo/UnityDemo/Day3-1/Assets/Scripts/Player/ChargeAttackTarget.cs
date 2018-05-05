using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackTarget : MonoBehaviour {
    public List<Collider> targetList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<Enemyhealth>().isDead)
                targetList.Add(other);
        }
    }

    // Use this for initialization
    void Awake () {
        targetList = new List<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
