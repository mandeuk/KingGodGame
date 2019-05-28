using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCloudPosition : MonoBehaviour {
    public static StormCloudPosition instance = null;
    
	void Awake () {
        instance = this;
    }
}
