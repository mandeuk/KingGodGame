using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatContainer : MonoBehaviour {
    Material[] afterImageMat = new Material[3];
    public Material[] raphaelMat;
    Renderer render;

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < 3; i++)
            afterImageMat[i] = Resources.Load("Materials/AfterImageEffectMat") as Material;
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeAfterImage()
    {
        //render.material = afterImageMat[0];
        render.material.color = Color.black;
        if (render.materials.Length > 2)
        {
            render.materials[1].color = Color.black;
            render.materials[2].color = Color.black;
            //print(render.materials.Length);
            //print(render.materials[0].name);
            //print(render.materials[1].name);
            //print(render.materials[2].name);
            //render.materials[0] = afterImageMat[0];
            render.materials[1] = afterImageMat[1];
            render.materials[2] = afterImageMat[2];
        }
    }

    public void changeraphaelMat()
    {
        render.material.color = Color.white;
        if (render.materials.Length > 2)
        {
            render.materials[1].color = Color.white;
            render.materials[2].color = Color.white;
        }
    }
}
