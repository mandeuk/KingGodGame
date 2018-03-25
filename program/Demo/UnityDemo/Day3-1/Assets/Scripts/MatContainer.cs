using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatContainer : MonoBehaviour {
    Material afterImageMat;
    public Material[] raphaelMat;
    bool colorChangeOn;
    float flashTime = 0;
    AfterImageEffect raphaelEX;
    Renderer render;

    // Use this for initialization
    void Awake()
    {
        raphaelEX = GameObject.FindWithTag("Player").GetComponent<AfterImageEffect>();
        afterImageMat = Resources.Load("Materials/AfterImageEffectMat") as Material;
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raphaelEX.onVanish)
        {
            for (int i = 0; i < render.materials.Length; ++i)
                render.materials[i].SetColor("_Color", Color.Lerp(render.materials[i].GetColor("_Color"), raphaelMat[i].color, Time.deltaTime));
        }
    }

    public void changeAfterImage()
    {
        for (int i = 0; i < render.materials.Length; ++i)
        {
            render.materials[i].SetColor("_Color", afterImageMat.color);
            render.materials[i].shader = afterImageMat.shader;
        }
    }

    public void changeraphaelMat()
    {
        flashTime = 0;
        for (int i = 0; i < render.materials.Length; ++i)
        {
            //colorChangeOn = true;
            //render.materials[i].SetColor("_Color", Color.Lerp(afterImageMat.color, Color.white, Time.deltaTime * 8));
            //render.material.color = Color.Lerp(afterImageMat.color, raphaelMat[i].color, flashTime);
            render.materials[i].shader = raphaelMat[i].shader;
        }
    }
}
