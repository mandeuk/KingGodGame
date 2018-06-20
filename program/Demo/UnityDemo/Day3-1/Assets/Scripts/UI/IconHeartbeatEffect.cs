using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHeartbeatEffect : MonoBehaviour {

    public UnityEngine.UI.Image iconimage;
    bool isScaleSmaller;

    // Use this for initialization
    void Start () {
        if (iconimage != null)
        {
            isScaleSmaller = true;
            StartCoroutine(Heartbeat());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Heartbeat()
    {
        while (true)
        {
            yield return null;
        }
    }
}
