using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyMovement : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            PlayerBase.instance.SetStatus(1, true, PlayerBase.instance.Energy);
            SoundManager.playEnergyGet();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
