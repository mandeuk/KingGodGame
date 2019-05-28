using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corosushead : MonoBehaviour {
    public Transform headObj;

    private void FixedUpdate()
    {
        if (headObj != null)
        {
            transform.position = headObj.position;
        }
    }
}
