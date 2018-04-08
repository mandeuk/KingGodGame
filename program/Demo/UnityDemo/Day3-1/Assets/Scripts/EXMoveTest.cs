using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMoveTest : MonoBehaviour {
    public GameObject Test1;
    public GameObject Test2;
    RaycastHit hit;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //if (Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 10))
            //{
            //    float dist = Vector3.Distance(hit.point, transform.position);
            //    Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, dist - 0.3f);

            //    transform.position = new Vector3(exPos.x, transform.position.y, exPos.z);
            //}

            //else if (Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 9))
            //{
            //    float dist = Vector3.Distance(hit.point, transform.position);
            //    Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, dist - 0.4f);


            //    transform.position = new Vector3(exPos.x, transform.position.y, exPos.z);
            //}



           // Vector3 expos2 = Vector3.MoveTowards(transform.position - transform.forward, transform.forward * 5, 70);

            //transform.position = new Vector3(expos2.x + transform.position.x, 0, expos2.z + transform.position.z);

            makeExPos();
        }
	}

    void makeExPos()
    {
        Test1.transform.position = transform.position + transform.up /2 - transform.forward * 1f;

        Gizmos.color = Color.red;

        if (Physics.BoxCast(Test1.transform.position, new Vector3(3, 0.1f, 0.1f), transform.forward, out hit, transform.rotation, 7, 1 << 10))
        {
            float dist = Vector3.Distance(hit.point, transform.position);
            //Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, 100);
            Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, transform.forward * (dist - 2f), 100);
            

            //transform.position = new Vector3(exPos.x + transform.position.x, 0, exPos.z + transform.position.z);
            Test2.transform.position = new Vector3(exPos.x + transform.position.x, 0, exPos.z + transform.position.z);
            //transform.position = new Vector3(exPos.x, 0, exPos.z);
            //print(dist);
            print(hit.distance);
        }
        else if (Physics.Raycast(transform.position + transform.up / 2 - transform.forward * 1f, transform.forward, out hit, 7, 1 << 9))
        //else if (Physics.BoxCast(Test1.transform.position, new Vector3(0.3f, 0.3f, 0.1f), transform.forward, out hit, transform.rotation, 7, 1 << 9))
        {
            float dist = Vector3.Distance(hit.point, transform.position);
            //Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, 100);
            Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, transform.forward * (dist - 1f), 100);

            //transform.position = new Vector3(exPos.x + transform.position.x, 0, exPos.z + transform.position.z);
            Test2.transform.position = new Vector3(exPos.x + transform.position.x, 0, exPos.z + transform.position.z);
            //transform.position = new Vector3(exPos.x, 0, exPos.z);
            //print(dist);
            print(hit.distance);
        }

        else
        {
            Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, transform.forward * 6, 100);

            //transform.position = new Vector3(exPos.x + transform.position.x, 0, exPos.z + transform.position.z);
            Test2.transform.position = new Vector3(exPos.x + transform.position.x, 0, exPos.z + transform.position.z);
        }


        //if (!Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 10))
        //    Physics.BoxCast(transform.position - transform.forward, new Vector3(0.6f, 1, 1), transform.forward, out hit, transform.rotation, 7, 1 << 9);

        //float dist = Vector3.Distance(hit.point, transform.position);
        //Vector3 exPos = Vector3.MoveTowards(transform.position - transform.forward, hit.point, dist - 0.3f);

        //return new Vector3(exPos.x, transform.position.y, exPos.z);
    }
}
