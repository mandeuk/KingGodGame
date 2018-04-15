using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCol : MonoBehaviour {
    public bool playerInDoor  = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            playerInDoor = true;
    }

    // Use this for initialization
    void Start () {
		
	}
}
