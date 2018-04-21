using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleData : MonoBehaviour {
    public List<GameObject> EnemyClones = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "EnemySpawn")
            {
                InitWraith();
            }
        }
    }

    private void Start()
    {
        int j = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "EnemySpawn")
            {
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = false;
                EnemyClones[j].transform.position = transform.GetChild(i).transform.position;
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = true;
                j++;
            }
        }
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

    public void InitWraith()
    {
        EnemyClones.Add(transform.GetComponent<EnemySpawn>().spawnWraith());
    }

    public void SetPosition()
    {
        int j = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "EnemySpawn")
            {
                EnemyClones[j].transform.position = transform.GetChild(i).transform.position;
                j++;
            }
        }
    }
}
