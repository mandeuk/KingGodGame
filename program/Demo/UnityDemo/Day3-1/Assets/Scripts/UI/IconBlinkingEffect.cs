using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBlinkingEffect : MonoBehaviour {

    public UnityEngine.UI.Image iconimage;
    Color iconcolor;
    bool isColorDarker;

    // Use this for initialization
    void Start () {
        if(iconimage != null)
        {
            iconcolor = iconimage.color;
            isColorDarker = true;
            StartCoroutine(Blinking());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Blinking()
    {
        while (true)
        {
            if (isColorDarker)
            {
                if (iconcolor.r > 0.3f)
                {
                    iconcolor.r -= 0.02f;
                    iconcolor.g -= 0.02f;
                    iconcolor.b -= 0.02f;
                }
                else
                {
                    isColorDarker = false;
                }
            }
            else
            {
                if (iconcolor.r < 1.0f)
                {
                    iconcolor.r += 0.02f;
                    iconcolor.g += 0.02f;
                    iconcolor.b += 0.02f;
                }
                else
                {
                    yield return new WaitForSeconds(1.0f);
                    isColorDarker = true;
                    
                }
            }
            iconimage.color = iconcolor;
            yield return null;
        }
    }
}
