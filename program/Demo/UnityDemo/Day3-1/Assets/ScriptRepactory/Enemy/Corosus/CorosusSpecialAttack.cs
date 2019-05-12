using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CorosusSpecialAttack : MonoBehaviour {
    List<GameObject> tarClones  = new List<GameObject>();

    private void Awake()
    {
        InitTar();
    }

    private void OnEnable()
    {
        EventManager.jumpAttackEventCall += new CorosusJumpAttackEventHandler(DropTar);
    }

    private void OnDisable()
    {
        EventManager.jumpAttackEventCall -= new CorosusJumpAttackEventHandler(DropTar);
    }

    void DropTar()
    {
        print("DropTar");

        //for (int i = 0; i < 10; i++)
        //    SpawnCorosusTar();
    }

    public void InitTar()
    {
        for (int i = 0; i < 10; i++)
            tarClones.Add(SpawnCorosusTar());
    }

    public GameObject SpawnCorosusTar()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/IceTar"), transform) as GameObject;
        enemyClone.GetComponent<NavMeshAgent>().enabled = false;
        enemyClone.transform.position = Vector3.up * 7f + Vector3.right  * Random.Range(-10,10) +  Vector3.forward * Random.Range(-10,10);

        return enemyClone;
    }
}