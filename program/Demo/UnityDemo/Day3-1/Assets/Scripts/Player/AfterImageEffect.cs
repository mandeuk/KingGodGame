using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour {
    public GameObject[] matCon = new GameObject[5];
    public GameObject EXMovePos;
    public bool onVanish = false;
    Animator avatar;
    Rigidbody rig;

	// Use this for initialization
	void Awake () {
        avatar = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //rig.Sleep();
            //StartCoroutine(EXMove());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //foreach (GameObject a in matCon)
                //a.changeraphaelMat();
        }
    }

    public IEnumerator EXMove()
    {
        foreach (GameObject a in matCon)
        {
            //a.GetComponent<GameObject>().SetActive(false);
            //a.changeAfterImage();
        }
        avatar.speed = 0;
        rig.Sleep();

        yield return new WaitForSeconds(.5f);
        onVanish = true;
        avatar.SetTrigger("EXMoveOn");
        transform.position = EXMovePos.transform.position;
        foreach (GameObject a in matCon)
        {
            a.GetComponent<GameObject>().SetActive(false);
            //a.changeraphaelMat();
        }
        avatar.speed = 1;

        yield return new WaitForSeconds(1.5f);
        onVanish = false;

        yield break;
    }
}
