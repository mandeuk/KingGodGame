using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChange : MonoBehaviour {
    public static PlayerColorChange instance = null;
    public GameObject[] rendObjs = new GameObject[5];
    Material[,] raphaelMats = new Material[5, 5];

    // Use this for initialization
    void Awake () {
        instance = this;

        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                raphaelMats[i, j] = Instantiate(rendObjs[i].GetComponent<Renderer>().materials[j]) as Material;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    public IEnumerator ColorChange()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].GetComponent<Renderer>().materials[j].color = new Vector4(0, 0, 0, 0);
            }
        }

        float timer = new float();
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                        "_Color", Color.Lerp(rendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_Color"),
                        raphaelMats[i, j].color, Time.deltaTime * 2.0f));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
