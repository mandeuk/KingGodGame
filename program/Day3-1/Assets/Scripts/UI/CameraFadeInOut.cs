using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeInOut : MonoBehaviour {
    public static CameraFadeInOut instance;

    IEnumerator fadeInCo;
    IEnumerator fadeOutCo;
    Material color;
    
	void Awake () {
        instance = this;
        color = transform.GetComponent<Renderer>().material;
        if (!GameManager.instance.testMod)
            color.color = new Vector4(0, 0, 0, 1);
        else
            color.color = new Vector4(1, 1, 1, 0);
    }

    private void Start()
    {
        if(!GameManager.instance.testMod)
            FadeIn();
    }

    public IEnumerator FadeOutUpdate()
    {
        float timer = new float();
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            color.SetColor("_Color", Color.Lerp(color.GetColor("_Color"), new Vector4(0, 0, 0, 1), Time.deltaTime * 7f));
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    public IEnumerator FadeInUpdate()
    {
        float timer = new float();
        color.color = new Vector4(0, 0, 0, 1);
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            color.SetColor("_Color", Color.Lerp(color.GetColor("_Color"), new Vector4(0, 0, 0, 0), Time.deltaTime * 7f));
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    public void FadeOut()
    {
        //StopCoroutine(FadeOutUpdate());
        //StopCoroutine(FadeInUpdate());
        StartCoroutine(FadeOutUpdate());
    }

    public void FadeIn()
    {
        //StopCoroutine(FadeInUpdate());
        //StopCoroutine(FadeOutUpdate());
        StartCoroutine(FadeInUpdate());
    }
}
