using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    
    GameObject slash1;

    public void PlayEffect1()
    {
        slash1.SetActive(true);
        slash1.transform.position = transform.position;
        slash1.GetComponent<ParticleSystem>().Play();
        //slash1.SetActive(false);
    }

    // Use this for initialization
    void Awake() {
        slash1 = Instantiate(Resources.Load("Prefabs/slash1")) as GameObject;
        slash1.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
