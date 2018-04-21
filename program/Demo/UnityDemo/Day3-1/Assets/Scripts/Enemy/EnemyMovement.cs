using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    GameObject player;
    NavMeshAgent nav;
    float speed = 2;
    float angleSpeed = 200;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled)
            nav.destination = player.transform.position;
    }


    public void stopMove()
    {
        nav.speed = 0;
        nav.angularSpeed = 0;
    }

    public void startMove()
    {
        nav.speed = speed;
        nav.angularSpeed = angleSpeed;
    }
}
