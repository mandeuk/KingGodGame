using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackTarget : MonoBehaviour {
    public List<Collider> targetList = new List<Collider>();
    public List<Collider> enemyBulletList = new List<Collider>();
    public List<Collider> anotherTargetList = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<ObjectBase>().isDead)
                targetList.Add(other);
        }

        if (other.CompareTag("EnemyBullet"))
        {
            enemyBulletList.Add(other);
        }

        if (other.CompareTag("Obstacle"))
        {
            anotherTargetList.Add(other);
        }
    }

    // Use this for initialization
    void Awake () {
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetComponent<ObjectBase>().isDead)
            {
                targetList.Remove(targetList[i]);
            }
        }

        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (!enemyBulletList[i].GetComponent<WraithBullet>().onFire)
            {
                enemyBulletList.Remove(enemyBulletList[i]);
            }
        }
    }
}
