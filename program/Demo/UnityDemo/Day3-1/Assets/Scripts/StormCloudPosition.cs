using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloudPosition : MonoBehaviour {
    public static StormCloudPosition instance = null;

	// Use this for initialization
	void Awake () {
        instance = this;
    }
}
