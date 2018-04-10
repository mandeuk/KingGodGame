using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoTest : MonoBehaviour {
    public static CoTest instance;

	// Use this for initialization
	void Awake () {
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public IEnumerator Test12()
    {
        while (true)
        {
            print("123");

            yield return new WaitForSeconds(1f);
        }
    }
}
