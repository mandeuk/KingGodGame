using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSword : MonoBehaviour {
    public GameObject smallSword;
    GameObject smallSwordClone;
    DamageNode damageNode;
    GameObject player;

    private void OnEnable()
    {
        player = PlayerBase.instance.gameObject;
        damageNode = new DamageNode(10, player, 0.1f, 10, 1);
        smallSwordClone = Instantiate(smallSword,transform);
        smallSwordClone.GetComponent<AttackTrigger>().damageNode = damageNode;
        smallSwordClone.transform.position = transform.position + player.transform.forward * 1.2f + transform.up;

        transform.Rotate(0, 120, 0);

        smallSwordClone = Instantiate(smallSword, transform);
        smallSwordClone.GetComponent<AttackTrigger>().damageNode = damageNode;
        //smallSwordClone.transform.rotation = transform.rotation;
        smallSwordClone.transform.position = transform.position + player.transform.forward * 1.2f + transform.up;

        transform.Rotate(0, 120, 0);

        smallSwordClone = Instantiate(smallSword, transform);
        smallSwordClone.GetComponent<AttackTrigger>().damageNode = damageNode;
        smallSwordClone.transform.rotation = transform.rotation;
        smallSwordClone.transform.position = transform.position + player.transform.forward * 1.2f + transform.up;
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
        transform.Rotate(new Vector3(0, 25, 0) * Time.deltaTime);
    }
}
