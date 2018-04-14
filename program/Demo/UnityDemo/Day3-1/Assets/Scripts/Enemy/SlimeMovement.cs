using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeMovement : MonoBehaviour {

    Transform Player;
    NavMeshAgent nav;

	// Use this for initialization
	void Awake () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (nav.enabled)
        {
            //nav.SetDestination(Player.position);
        }
	}
}
