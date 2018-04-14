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
		if(EnemyClones.Count < 1)
        {
            transform.parent.GetComponentInChildren<RoomData>().RoomClear();
        }

        for (int i = 0; i < EnemyClones.Count; i++)
        {
            if (EnemyClones[i].GetComponent<SlimeHealth>().isSinking)
            {
                EnemyClones.Remove(EnemyClones[i]);
            }
        }
    }
}
