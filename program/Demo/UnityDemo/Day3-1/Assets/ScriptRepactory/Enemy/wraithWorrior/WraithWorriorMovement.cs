using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WraithWorriorMovement : MonoBehaviour {
    GameObject player;
    NavMeshAgent nav;
    Animator anim;
    EnemyStatus status;

    public bool playerIn;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        status = GetComponent<EnemyStatus>();
        nav.speed = 3;
        nav.angularSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled && playerIn && !GetComponent<WraithWorriorAttack>().attacking)
        {
            nav.destination = player.transform.position;
            if (Vector3.Distance(player.transform.position, transform.position) > nav.stoppingDistance /*status.attackRange*/)
            {
                nav.speed = GetComponent<EnemyStatus>().moveSpeed;
                anim.SetBool("Walk", true);
            }

            else
            {
                nav.speed = 0;
                anim.SetBool("Walk", false);
            }
        }
    }

    public void stopMove()
    {
        nav.speed = 0;
        nav.angularSpeed = 0;
    }

    public void startMove()
    {
        nav.speed = GetComponent<EnemyStatus>().moveSpeed;
    }

    public void StopWalk()
    {
        nav.speed = 0;
    }
}
