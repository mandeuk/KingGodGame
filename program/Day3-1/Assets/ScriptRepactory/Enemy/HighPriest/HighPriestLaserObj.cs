using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestLaserObj : MonoBehaviour {
    public GameObject beforeLaser;
    public GameObject rightLaser;
    public GameObject leftLaser;
    public DamageNode damageNode;

    bool hit = false;

    public void PlayerHit()
    {
        rightLaser.GetComponent<Collider>().enabled = false;
        leftLaser.GetComponent<Collider>().enabled = false;
        hit = true;
    }

    public void LaserShot()
    {
        rightLaser.GetComponent<Collider>().enabled = false;
        leftLaser.GetComponent<Collider>().enabled = false;
        StartCoroutine(Laser());
    }

    IEnumerator Laser()
    {
        rightLaser.GetComponent<AttackTrigger>().damageNode = damageNode;
        leftLaser.GetComponent<AttackTrigger>().damageNode = damageNode;

        yield return new WaitForSeconds(0.2f);
        rightLaser.transform.rotation = new Quaternion(0, 0, 0, 0);
        leftLaser.transform.rotation = new Quaternion(0, 0, 0, 0);
        rightLaser.SetActive(true);
        leftLaser.SetActive(true);
        beforeLaser.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        rightLaser.GetComponent<Collider>().enabled = true;
        SoundManager.playHighPriestLaserAttack();

        yield return new WaitForSeconds(1.0f);
        if(!hit)
        {
            leftLaser.GetComponent<Collider>().enabled = true;
        }

        float timer = new float();
        while (timer < 1.2f)
        {
            timer += Time.fixedDeltaTime;
            rightLaser.transform.Rotate(0, 70 * Time.fixedDeltaTime, 0);
            leftLaser.transform.Rotate(0, -70 * Time.fixedDeltaTime, 0);
            yield return new WaitForFixedUpdate();
        }
        rightLaser.GetComponent<Collider>().enabled = false;
        leftLaser.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(1.0f);
        rightLaser.SetActive(false);
        leftLaser.SetActive(false);
        beforeLaser.SetActive(false);
        hit = false;

        yield break;
    }
}
