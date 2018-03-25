using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXMove : MonoBehaviour {
    public GameObject[] rendObjs = new GameObject[5];

    Material transMat;
    Material afterImageMat;
    Material test;
    Material[,] raphaelMats = new Material[5,5];
    Material[] changeMats;



    // Use this for initialization
    void Awake () {
        afterImageMat = Resources.Load("Materials/AfterImageEffectMat") as Material;
        transMat = Resources.Load("Materials/Transparent") as Material;

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                raphaelMats[i, j] = rendObjs[i].GetComponent<Renderer>().materials[j];
            }
        }

        //for (int i = 0; i < rendObjs.Length; ++i)
        //{
        //    for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
        //    {
        //        changeMats = rendObjs[i].GetComponent<Renderer>().materials;
        //        changeMats[j] = transMat;
        //        rendObjs[i].GetComponent<Renderer>().materials = changeMats;
        //        rendObjs[i].GetComponent<Renderer>().materials[j].color = new Vector4(1,1,1,0);
        //    }
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
