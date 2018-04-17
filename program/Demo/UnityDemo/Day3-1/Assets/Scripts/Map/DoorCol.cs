using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCol : MonoBehaviour {
    public bool playerInDoor  = false;
    public int doorType = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInDoor = true;
            StartCoroutine(GetComponentInParent<RoomData>().RoomMove(doorType));
        }
    }

    // Use this for initialization
    void Awake () {
		
	}
}
