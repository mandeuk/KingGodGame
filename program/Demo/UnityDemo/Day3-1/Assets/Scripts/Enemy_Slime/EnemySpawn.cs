using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public List<GameObject> enemyCloneList = new List<GameObject>();
    public List<GameObject> deadenemyCloneList = new List<GameObject>();

    //public IEnumerator PlayEffect(int stateNum)
    //{
    //    usedenemyCloneList.Add(enemyCloneList[0]);
    //    enemyCloneList[0].SetActive(true);
    //    enemyCloneList[0].transform.position = transform.position + Vector3.up*0.7f;
    //    enemyCloneList[0].transform.localScale = new Vector3(1f, 1f, 1f);
    //    enemyCloneList[0].transform.Rotate(new Vector3(0, 0, Random.Range(-30,30)));

    //    if(stateNum == 2)
    //    {
    //        enemyCloneList[0].transform.Rotate(new Vector3(0, 0, 180));
    //    }

    //    if(stateNum == 4)
    //    {
    //        enemyCloneList[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //    }

    //    enemyCloneList.RemoveAt(0);

    //    yield return new WaitForSeconds(1f);

    //    enemyCloneList.Add(usedenemyCloneList[0]);
    //    usedenemyCloneList[0].SetActive(false);
    //    usedenemyCloneList.RemoveAt(0);

    //    yield break;
    //}

    // Use this for initialization
    void Awake()
    {
        initEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enableEnemy()
    {
        enemyCloneList.Add(deadenemyCloneList[0]);
        deadenemyCloneList[0].SetActive(false);
        deadenemyCloneList.RemoveAt(0);
    }

    public void initEnemy()
    {
        for (int i = 0; i < 20; ++i)
            deadenemyCloneList.Add(spawnEnemy());
    }

    public GameObject spawnEnemy()
    {
        GameObject enemyClone = Instantiate(Resources.Load("Prefabs/Enemy/Slime")) as GameObject;

        return enemyClone;
    }
}
