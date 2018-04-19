using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour {
    public List<GameObject> EnemyClones = new List<GameObject>();

	// Use this for initialization
	void Awake () {

    }

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < EnemyClones.Count; i++)
        {
            if (EnemyClones[i].GetComponent<Enemyhealth>().isDead)
            {
                EnemyClones.Remove(EnemyClones[i]);
            }   // 사망여부 판단.
        }
    }
}
