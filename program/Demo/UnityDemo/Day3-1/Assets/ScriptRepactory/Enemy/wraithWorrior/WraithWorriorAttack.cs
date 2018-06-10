using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithWorriorAttack : MonoBehaviour {

    GameObject player;
    Vector3 playerPostion;
    Animator anim;
    EnemyStatus status;

    public bool attacking;
    public bool turning;
    public bool targetPlayer;

    // Use this for initialization
    void Awake()
    {
        attacking = false;
        turning = false;

        player = PlayerBase.instance.gameObject;
        anim = GetComponent<Animator>();
        status = GetComponent<EnemyStatus>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
            Turn();

        if (Vector3.Distance(player.transform.position, transform.position) < status.attackRange && !turning)
        {
            StartAttack();
        }
        else
        {
            StopAttack();
        }
    }

    public void StartAttack()
    {
        attacking = true;
        anim.SetBool("Attack", true);
    }

    public void StopAttack()
    {
        //attacking = false;
        anim.SetBool("Attack", false);
    }

    void Turn()
    {
        int turnDir;
        playerPostion = player.transform.position;
        Vector3 forwardPos = transform.position + transform.forward;

        Vector3 forwardVec = forwardPos - transform.position;
        Vector3 diff = playerPostion - transform.position;

        turnDir = Turnjudge(forwardVec.normalized, diff.normalized);

        if (Vector3.Angle(forwardVec.normalized, diff.normalized) > 10.0f)
        {
            turning = true;
            if (!attacking)
            {
                transform.Rotate(new Vector3(0, turnDir * 1 * 100, 0) * Time.deltaTime*2);
            }
        }
        else
        {
            transform.LookAt(player.transform.position);
            turning = false;
        }
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }

    //public void WorriorAttack()
    //{
    //    StartCoroutine(EnemyEffect.instance.PlayWraithWorriorAttackEffect(gameObject));
    //    targetPlayer = GetComponentInChildren<WorriorTarget>().targetPlayer;
    //    if (targetPlayer)
    //    {
    //        if (!player.GetComponent<PlayerHealth>().isDead)
    //        {
    //            player.GetComponent<PlayerHealth>().PlayerDamaged(gameObject);
    //        }
    //    }
    //}
}
