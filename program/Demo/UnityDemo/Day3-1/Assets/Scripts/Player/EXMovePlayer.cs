using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMovePlayer : MonoBehaviour {

    RaycastHit hit;
    Vector3 ExMovePos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
    }

    public IEnumerator EXMove()
    {

        yield break;
    }

    void judgeFrontWall()
    {

    }

    Vector3 makeExPos()
    {
        if (Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 10))
        {
            float dist = Vector3.Distance(hit.point, transform.position);
            Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, 100);

            return new Vector3(exPos.x, 0, exPos.z);
        }

        else if (Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 9))
        {
            float dist = Vector3.Distance(hit.point, transform.position);
            Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, 100);


            return new Vector3(exPos.x, 0, exPos.z);
        }

        else
        {
            Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, transform.forward * 6, 100);

            return new Vector3(exPos.x, 0, exPos.z);
        }


        //if (!Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 10))
        //    Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 9);

        //float dist = Vector3.Distance(hit.point, transform.position);
        //Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, dist - 0.3f);

        //return new Vector3(exPos.x, transform.position.y, exPos.z);
    }
}
