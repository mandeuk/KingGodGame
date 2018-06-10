using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{


    GameObject explosionAttackEffect;
    public GameObject chargeEffect; // 외부에서(chargeAttack FBX 마지막) 꺼줘야해서 public으로 선언.
    GameObject chargeEndEffect;



    public void playBlinkEffect()
    {

    }

    public void playExplosionAttackEffect()
    {
        Quaternion test2 = Quaternion.LookRotation(GetComponent<PlayerMovement>().movePos.normalized);

        if (transform.CompareTag("Player"))
        {
            explosionAttackEffect.SetActive(false);
            explosionAttackEffect.SetActive(true);
            explosionAttackEffect.transform.position = transform.position + Vector3.up * 0.1f;
            //explosionAttackEffect.transform.rotation = Quaternion.Euler(-90, EXMovePos.transform.rotation.eulerAngles.y, 0);
            explosionAttackEffect.transform.rotation = Quaternion.Euler(-90, test2.eulerAngles.y, 0);
            explosionAttackEffect.GetComponent<ParticleSystem>().Play();
        }
    }
    public void playChargeAttackChargeEffect()
    {

        if (transform.CompareTag("Player"))
        {
            chargeEffect.SetActive(false);
            chargeEffect.SetActive(true);
            chargeEffect.transform.position = transform.position + Vector3.up * 1.4f - transform.forward * 0.15f;
            chargeEffect.GetComponent<ParticleSystem>().Play();
        }
    }
    public void playChargeAttackEndEffect()
    {

        if (transform.CompareTag("Player"))
        {
            chargeEffect.SetActive(false);
            chargeEndEffect.SetActive(false);
            chargeEndEffect.SetActive(true);
            chargeEndEffect.transform.position = transform.position + Vector3.up;
            chargeEndEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    void Awake()
    {
        explosionAttackEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackEffect"), transform) as GameObject;
        chargeEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackChargeEffect2"), transform) as GameObject;
        chargeEndEffect = Instantiate(Resources.Load("Prefabs/Effect/ChargeAttackEndEffect"), transform) as GameObject;
    }
}
