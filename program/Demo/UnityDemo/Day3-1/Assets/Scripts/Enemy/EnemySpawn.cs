using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    List<GameObject> enemyCloneList;

    // Use this for initialization
    void Awake()
    {
        enemyCloneList = transform.GetComponent<ObstacleData>().EnemyClones;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "EnemySpawn")
                InitHighPriest(transform.GetChild(i).transform.position);
        }
    }

    public void InitHighPriest(Vector3 pos)
    {
        enemyCloneList.Add(spawnHighpriest(pos));
    }

    public GameObject spawnHighpriest(Vector3 pos)
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/highpriest"),transform) as GameObject;
        enemyClone.transform.position = pos + Vector3.up*1.1f;

        return enemyClone;
    }
}
