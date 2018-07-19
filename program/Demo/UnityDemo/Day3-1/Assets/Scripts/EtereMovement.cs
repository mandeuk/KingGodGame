using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtereMovement : MonoBehaviour {
    GameObject player;
    Rigidbody rigid;
    Vector3 dir;
    Collider col;
    float speed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            StopCoroutine(EtereMove());
            gameObject.SetActive(false);
            PlayerBase.instance.SetStatus(1, true, PlayerBase.instance.Etere);
            EnergyManager.instance.ReturnEtere(gameObject);
        }
    }

    private void OnEnable()
    {
        col = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        player = PlayerBase.instance.gameObject;
        speed = 2;
        StartCoroutine(EtereMove());
    }
    
    private IEnumerator EtereMove()
    {
        yield return new WaitForSeconds(1.0f);
        rigid.Sleep();
        col.isTrigger = true;
        rigid.useGravity = false;

        yield return new WaitForSeconds(1.0f);               
        while (true)
        {            
            dir = ((player.transform.position + Vector3.up) - transform.position).normalized;
            rigid.MovePosition(transform.position + dir * speed * Time.deltaTime);
            speed += 0.1f;

            yield return new WaitForFixedUpdate();
        }
    }
}
