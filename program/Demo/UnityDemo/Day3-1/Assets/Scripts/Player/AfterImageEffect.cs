using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageEffect : MonoBehaviour {
    public MatContainer[] matCon = new MatContainer[5];
    public GameObject EXMovePos;
    public bool onVanish = false;
    Animator avatar;

	// Use this for initialization
	void Awake () {
        avatar = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(EXMove());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (MatContainer a in matCon)
                a.changeraphaelMat();
        }
    }

    public IEnumerator EXMove()
    {
        foreach (MatContainer a in matCon)
            a.changeAfterImage();
        avatar.speed = 0;

        yield return new WaitForSeconds(.4f);
        onVanish = true;
        avatar.SetTrigger("EXMoveOn");
        transform.position = EXMovePos.transform.position;
        foreach (MatContainer a in matCon)
            a.changeraphaelMat();
        avatar.speed = 1;

        yield return new WaitForSeconds(2f);
        onVanish = false;

        yield break;
    }
}
