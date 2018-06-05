using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour {
    // Use this for initialization
    // Start로 생성해줘야 빌드할때 네비메쉬가 생긴 후에 몬스터 배치가됨.
    
    private void Awake()
    {

    }

    void Start()
    {

    }

    public GameObject spawnHighpriest(Vector3 pos)
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/highpriest"),transform) as GameObject;
        enemyClone.transform.position = pos + Vector3.up*1.1f;

        return enemyClone;
    }

    public  GameObject spawnWraith()
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
}
