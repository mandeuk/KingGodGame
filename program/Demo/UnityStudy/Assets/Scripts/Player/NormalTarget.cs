using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour {
    public List<Collider> targetList;

	// Use this for initialization
	void Awake () {
        targetList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(!other.GetComponent<SlimeHealth>().isSinking)
                targetList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other);
    }

    // Update is called once per frame
    void Update () {
        //foreach(Collider one in targetList)
        //for(Collider i = targetList[0]; i<targetList.Count)
        for(int i = 0; i<targetList.Count; i++)
        {
            if (targetList[i].GetComponent<SlimeHealth>().isSinking)
            {
                targetList.Remove(targetList[i]);
            }
        }
    }
}
