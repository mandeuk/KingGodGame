using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChange : MonoBehaviour {
    public static PlayerColorChange instance = null;
    public GameObject[] rendObjs = new GameObject[5];
    Material[,] raphaelMats = new Material[5, 5];

    Color rimBlue;
    float bodyRimPow, hairRimPow, swordRimPow, tailRimPow;
    float curBodyRimPow, curHairRimPow, curSwordRimPow, curTailRimPow;

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

        rimBlue = raphaelMats[0, 0].GetColor("_rimColor");

        bodyRimPow = raphaelMats[0, 0].GetFloat("_rimPow");
        swordRimPow = raphaelMats[0, 2].GetFloat("_rimPow");
        hairRimPow = raphaelMats[1, 0].GetFloat("_rimPow");
        tailRimPow = raphaelMats[3, 0].GetFloat("_rimPow");
    }

    public void PlayerColorChangeBlack()
    {
        StopCoroutine(ColorChange());
        StartCoroutine(ColorChange());
    }

    public void PlayerColorChangeWhite()
    {
        StopCoroutine(ColorWhiteChange());

        StartCoroutine(ColorWhiteChange());
    }

    public void PlayerColorChangeYellow()
    {
        StopCoroutine(ColorRimYellowChange());
        StopCoroutine(RimPowChange());

        StartCoroutine(ColorRimYellowChange());
        StartCoroutine(RimPowChange());
    }

    public void PlayerColorChangeBlue()
    {
        StopCoroutine(RimPowChange());
        StartCoroutine(RimPowChange());
    }

    public void PlayerColorChangePurple()
    {
        StopCoroutine(ColorRimPurPleChange());
        StopCoroutine(RimPowChange());

        StartCoroutine(ColorRimPurPleChange());
        StartCoroutine(RimPowChange());
    }

    IEnumerator ColorChange()
    {
        ColorBlack();

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
                        raphaelMats[i, j].color, Time.deltaTime * 1.0f));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator ColorWhiteChange()
    {
        ColorWhite();

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

    // 림컬러를 노란색으로 바꾸었다가 점점 본래색을 찾게하는 기능
    IEnumerator ColorRimYellowChange()
    {
        RimColorYellow();

        float timer = new float();
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                        "_rimColor", Color.Lerp(rendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_rimColor"),
                        raphaelMats[i, j].GetColor("_rimColor"), Time.deltaTime * 2.0f));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator ColorRimRedChange()
    {
        RimColorRed();

        float timer = new float();
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                        "_rimColor", Color.Lerp(rendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_rimColor"),
                        raphaelMats[i, j].GetColor("_rimColor"), Time.deltaTime * 2.0f));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator ColorRimPurPleChange()
    {
        RimColorPurple();

        yield return new WaitForSeconds(1.0f);
        float timer = new float();
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < rendObjs.Length; ++i)
            {
                for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
                {
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor(
                        "_rimColor", Color.Lerp(rendObjs[i].GetComponent<Renderer>().materials[j].GetColor("_rimColor"),
                        raphaelMats[i, j].GetColor("_rimColor"), Time.deltaTime * 2.0f));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    // rimPow의 값을 늘려서 플레이어가 림라이트를 많이 받게하는 기능
    IEnumerator RimPowChange()
    {
        curBodyRimPow = 0;
        curSwordRimPow = 0;
        curHairRimPow = 0;
        curTailRimPow = 0;

        rendObjs[0].GetComponent<Renderer>().materials[0].SetFloat("_rimPow", curBodyRimPow);
        rendObjs[0].GetComponent<Renderer>().materials[2].SetFloat("_rimPow", curSwordRimPow);
        rendObjs[1].GetComponent<Renderer>().materials[0].SetFloat("_rimPow", curHairRimPow);
        rendObjs[3].GetComponent<Renderer>().materials[0].SetFloat("_rimPow", curTailRimPow);

        float timer = new float();
        while (timer < 2f)
        {
            timer += Time.deltaTime;

            if (rendObjs[0].GetComponent<Renderer>().materials[0].GetFloat("_rimPow") < bodyRimPow)
            {
                rendObjs[0].GetComponent<Renderer>().materials[0].SetFloat("_rimPow", curBodyRimPow);
            }

            if (rendObjs[0].GetComponent<Renderer>().materials[2].GetFloat("_rimPow") < swordRimPow)
            { 
                rendObjs[0].GetComponent<Renderer>().materials[2].SetFloat("_rimPow", curSwordRimPow);
            }

            if (rendObjs[1].GetComponent<Renderer>().materials[0].GetFloat("_rimPow") < hairRimPow)
            {
                rendObjs[1].GetComponent<Renderer>().materials[0].SetFloat("_rimPow", curHairRimPow);
            }

            if (rendObjs[3].GetComponent<Renderer>().materials[0].GetFloat("_rimPow") < tailRimPow)
            {
                rendObjs[3].GetComponent<Renderer>().materials[0].SetFloat("_rimPow", curTailRimPow);
            }

            curBodyRimPow += Time.deltaTime * 3;
            curSwordRimPow += Time.deltaTime * 3;
            curHairRimPow += Time.deltaTime * 3;
            curTailRimPow += Time.deltaTime * 3;

            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    
    void ColorBlack()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].GetComponent<Renderer>().materials[j].color = new Vector4(0, 0, 0, 0);
            }
        }
    }

    void ColorWhite()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].GetComponent<Renderer>().materials[j].color = new Vector4(2, 2, 2,1);
            }
        }
    }

    void RimColorYellow()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                if (!(i == 0 && j == 1))
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor("_rimColor",Color.yellow);
            }
        }
    }

    public void RimColorRed()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                if (!(i == 0 && j == 1))
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor("_rimColor", Color.red);
            }
        }
    }

    public void RimColorPurple()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                if (!(i == 0 && j == 1))
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor("_rimColor", new Color(1,0,1));
            }
        }
    }

    public void RimColorOrigin()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                if (!(i == 0 && j == 1))
                    rendObjs[i].GetComponent<Renderer>().materials[j].SetColor("_rimColor", raphaelMats[i, j].GetColor("_rimColor"));
            }
        }
    }
    

    public void PlayerDisappear()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].SetActive(false);
            }
        }
    }

    public void PlayerAppear()
    {
        for (int i = 0; i < rendObjs.Length; ++i)
        {
            for (int j = 0; j < rendObjs[i].GetComponent<Renderer>().materials.Length; ++j)
            {
                rendObjs[i].SetActive(true);
            }
        }
    }

}
