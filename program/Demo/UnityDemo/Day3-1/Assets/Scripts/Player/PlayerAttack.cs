using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject normalAttacCam;
    public GameObject exMoveCam;
    protected Animator avatar;
    public bool b_attacking;
    public bool lotusOn = false;


    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);
        List<Collider> enemyBulletList = new List<Collider>(normalTarget.enemyBulletList);
        List<Collider> realTargetList = new List<Collider>();

        if (normalTarget.anotherTargetList.Count > 0)
        {
            foreach (Collider one in targetList)
            {
                if (normalTarget.anotherTargetList.Count > 0)
                {
                    if (!one.GetComponent<Enemyhealth>().CheckBtwRapObj())
                    {
                        realTargetList.Add(one);
                    }
                }
            }

            if (realTargetList.Count > 0)
            {
                StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in realTargetList)
            {
                Enemyhealth enemy = one.GetComponent<Enemyhealth>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.NormalDamaged(PlayerStatus.instance.attackPower, transform.position, 0.2f, 5f, stateNum));
                }
            }

        }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

        else
        {
            if (targetList.Count > 0)
            {
                //StopCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(0.02f));
                //StopCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(0.02f));
                StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in targetList)
            {
                Enemyhealth enemy = one.GetComponent<Enemyhealth>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.NormalDamaged(PlayerStatus.instance.attackPower, transform.position, 0.2f, 5f, stateNum));
                }
            }
        }

        if(enemyBulletList.Count > 0)
        {
            foreach (Collider one in enemyBulletList)
            {
                WraithEffect.instance.BulletHit(one.gameObject);
                one.GetComponent<WraithBullet>().onFire = false;
            }
        }
    }

    public void skillAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(skillTarget.targetList);
        List<Collider> realTargetList = new List<Collider>();

        if (skillTarget.anotherTargetList.Count > 0)
        {
            foreach (Collider one in targetList)
            {
                if (skillTarget.anotherTargetList.Count > 0)
                {
                    if (!one.GetComponent<Enemyhealth>().CheckBtwRapObj())
                    {
                        realTargetList.Add(one);
                    }
                }
            }

            if (realTargetList.Count > 0)
            {
                //StopCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(0.02f));
                //StopCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(0.02f));
                //exMoveCam.GetComponent<EXMoveCam>().CameraCamTurning();
                StartCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(targetList.Count * 0.02f));
                //StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in realTargetList)
            {
                Enemyhealth enemy = one.GetComponent<Enemyhealth>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.SkillDamaged(PlayerStatus.instance.attackPower, transform.position, 0.5f, 10f, stateNum));
                }
            }

        }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

        else
        {
            if (targetList.Count > 0)
            {
                //StopCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(0.02f));
                //StopCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(0.02f));
                //exMoveCam.GetComponent<EXMoveCam>().CameraCamTurning();
                StartCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(targetList.Count * 0.02f));
                //StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in targetList)
            {
                Enemyhealth enemy = one.GetComponent<Enemyhealth>();
                if (enemy != null && !enemy.isDead)
                {
                    StartCoroutine(enemy.SkillDamaged(PlayerStatus.instance.attackPower, transform.position, 0.5f, 10f, stateNum));
                }
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
        if (Input.GetKey(KeyCode.J))
        {
            OnAttacking();
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

            if (lotusOn) {
                //transform.GetComponent<PlayerEffect>().PlayBlackWaveEffect(stateNum);

                //StopCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(0.02f));
                //StopCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(0.02f));
                //exMoveCam.GetComponent<EXMoveCam>().CameraCamTurning();
                //StartCoroutine(exMoveCam.GetComponent<EXMoveCam>().cameraHitEvent(0.02f));
                //StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(0.02f));
            }
        }
    }
}
