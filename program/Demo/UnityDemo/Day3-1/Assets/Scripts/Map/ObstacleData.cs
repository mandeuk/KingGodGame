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
            if (transform.GetChild(i).name == "EnemySpawnPos")
            {
                InitWraith();
            }

            if(transform.GetChild(i).name == "WorriorSpawnPos")
            {
                InitWraithWorrior();
            }

            if (transform.GetChild(i).name == "WraithBossSpawnPos")
            {
                InitWraithBoss();
            }

            if (transform.GetChild(i).name == "CorosusSpawnPos")
            {
                InitCorosus();
            }
            if (transform.GetChild(i).name == "HighPriestSpawnPos")
            {
                InitHighPriest();
            }
        }
    }
    
    private void Start()
    {
        int j = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "EnemySpawnPos")
            {
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = false;
                EnemyClones[j].transform.position = transform.GetChild(i).transform.position;
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = true;
                j++;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "WorriorSpawnPos")
            {
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = false;
                EnemyClones[j].transform.position = transform.GetChild(i).transform.position;
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = true;
                j++;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "WraithBossSpawnPos")
            {
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = false;
                EnemyClones[j].transform.position = transform.GetChild(i).transform.position;
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = true;
                j++;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "CorosusSpawnPos")
            {
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = false;
                EnemyClones[j].transform.position = transform.GetChild(i).transform.position;
                EnemyClones[j].GetComponent<NavMeshAgent>().enabled = true;
                j++;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "HighPriestSpawnPos")
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
            if (EnemyClones[i].GetComponent<ObjectBase>().isDead)
            {
                EnemyClones.Remove(EnemyClones[i]);
            }   // 사망여부 판단.
        }
    }

    public void InitWraith()
    {
        EnemyClones.Add(spawnWraith());
    }

    public void InitWraithWorrior()
    {
        EnemyClones.Add(SpawnWraithWorrior());
    }

    public void InitWraithBoss()
    {
        EnemyClones.Add(SpawnWraithBoss());
    }

    public void InitCorosus()
    {
        EnemyClones.Add(SpawnCorosus());
    }

    public void InitHighPriest()
    {
        EnemyClones.Add(spawnHighpriest());
    }

    public GameObject spawnHighpriest()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/highpriest"), transform) as GameObject;
        enemyClone.transform.position = Vector3.up * 1.1f;

        return enemyClone;
    }

    public GameObject spawnWraith()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/wraith"), transform) as GameObject;
        enemyClone.transform.position = Vector3.up * 1.3f;

        return enemyClone;
    }

    public GameObject SpawnWraithWorrior()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/wraith_worrior"), transform) as GameObject;
        enemyClone.transform.position = Vector3.up * 1.3f;

        return enemyClone;
    }

    public GameObject SpawnWraithBoss()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/wraith_worrior_Boss"), transform) as GameObject;
        enemyClone.transform.position = Vector3.up * 1.8f;

        return enemyClone;
    }
    
    public GameObject SpawnCorosus()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/corosusFrost"), transform) as GameObject;
        enemyClone.transform.position = Vector3.up * 2f;

        return enemyClone;
    }
}
