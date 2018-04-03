using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject normalAttacCam;
    public GameObject exMoveCam;
    protected Animator avatar;
    public bool b_attacking;
    public bool enemyInList = false;

    public static int normalDamage = 10;
    public int skillDamage = 30;

    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);

        if (targetList.Count > 0)
        {
            StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.025f));
        }

        foreach (Collider one in targetList)
        {
            SlimeHealth slime = one.GetComponent<SlimeHealth>();
            if (slime != null && !slime.isSinking)
            {
                StartCoroutine(slime.StartDamage(normalDamage, transform.position, 0.5f, 3f, stateNum));
            }
        }
    }

    public void skillAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(skillTarget.targetList);

        if (targetList.Count > 0)
        {
            StartCoroutine(exMoveCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.025f));
        }

        foreach (Collider one in targetList)
        {
            SlimeHealth slime = one.GetComponent<SlimeHealth>();
            if (slime != null && !slime.isSinking)
            {
                StartCoroutine(slime.StartSkillDamage(normalDamage, transform.position, 0.3f, 3f, stateNum));
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        avatar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.K))
            {
                OnAttacking();
            }

            if (normalTarget.targetList.Count > 0)
            {
                enemyInList = true;
            }
            else
                enemyInList = false;
        }
    }

    public void OnAttacking()
    {
        avatar.SetBool("Combo", true);
        //avatar.SetBool("StartAttack",true);
    }

    public void StopAttacking()
    {
        avatar.SetBool("Combo", false);
        //avatar.SetBool("StartAttack", false);
    }

    public void NormalAttackEvent(int stateNum)
    {
        if (transform.CompareTag("Player") || b_attacking)
        {
            StartCoroutine(transform.GetComponent<PlayerEffect>().PlayEffect(stateNum));
            NormalAttack(stateNum);
        }
    }
}
