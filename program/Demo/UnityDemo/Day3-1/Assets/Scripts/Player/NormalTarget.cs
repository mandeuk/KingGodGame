﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour {
    public List<Collider> targetList;
    public List<Collider> anotherTargetList;
    public List<Collider> enemyBulletList;

	// Use this for initialization
	void Awake () {
        targetList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<Enemyhealth>().isDead)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<Enemyhealth>().isDead)
                targetList.Remove(other);
        }
        if (other.CompareTag("Obstacle"))
        {
            anotherTargetList.Remove(other);
        }
        if (other.CompareTag("EnemyBullet"))
        {
            enemyBulletList.Remove(other);
        }

    }

    // Update is called once per frame
    void Update () {
        for(int i = 0; i<targetList.Count; i++)
        {
            if (targetList[i].GetComponent<Enemyhealth>().isDead)
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
