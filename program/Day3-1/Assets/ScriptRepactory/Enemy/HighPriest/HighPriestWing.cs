using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestWing : MonoBehaviour {
    public Transform Pos;

    private void FixedUpdate()
    {
        if (Pos != null)
        {
            //transform.position = Pos.position;
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + Pos.rotation.z);
        }
    }
}
