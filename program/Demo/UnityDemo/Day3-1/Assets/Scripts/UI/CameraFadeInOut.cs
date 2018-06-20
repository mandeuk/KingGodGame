using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeInOut : MonoBehaviour {
    Material color;

	// Use this for initialization
	void Awake () {
        color = transform.GetComponent<Renderer>().material;
        color.color = new Vector4(0, 0, 0, 0);
    }

    public IEnumerator FadeOut()
    {
        float timer = new float();
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            color.SetColor("_Color", Color.Lerp(color.GetColor("_Color"), new Vector4(0, 0, 0, 1), Time.deltaTime * 3.5f));
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    public IEnumerator FadeIn()
    {
        float timer = new float();
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            color.SetColor("_Color", Color.Lerp(color.GetColor("_Color"), new Vector4(0, 0, 0, 0), Time.deltaTime * 3.5f));
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    public void ActiveFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}
