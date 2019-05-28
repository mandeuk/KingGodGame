using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEventManager : MonoBehaviour {
    public static CameraEventManager instance;

    PostProcessingBehaviour behaviour;

    VignetteModel.Settings vignetteOrigin = new VignetteModel.Settings();
    ChromaticAberrationModel.Settings chromaticOrigin = new ChromaticAberrationModel.Settings();

    VignetteModel.Settings vignette = new VignetteModel.Settings();
    ChromaticAberrationModel.Settings chromatic = new ChromaticAberrationModel.Settings();

    public AnimationCurve chromaticCurve;
    public AnimationCurve vignetteCurve;

    IEnumerator stopCo;

    private void Awake()
    {
        instance = this;
        
        behaviour = GetComponent<PostProcessingBehaviour>();
        //vignette = new VignetteModel.Settings();
        //chromatic = new ChromaticAberrationModel.Settings();
        // 초기값 셋팅.
        // 비네트의 초기값과 크로매틱의 초기값을 셋팅함

        vignette = behaviour.profile.vignette.settings;
        chromatic = behaviour.profile.chromaticAberration.settings;

        vignette.intensity = 0.5f;
        vignette.smoothness = 0.7f;
        vignette.color = Color.black;
        chromatic.intensity = 0.0f;

        behaviour.profile.vignette.settings = vignette;
        behaviour.profile.chromaticAberration.settings = chromatic;

        vignetteOrigin = behaviour.profile.vignette.settings;
        chromaticOrigin = behaviour.profile.chromaticAberration.settings;
    }

    //  맞았을시 빨갛게 되는 비네트
    public void VignetteColorRedChange()
    {
        StartCoroutine(VignetteColorRedChangeCo());
    }

    public void ChromaticChange()
    {
        print("ChromaticChange");
        StartCoroutine(ChromaticChangeCo());
    }

    public void DevillizationCameraEvent()
    {
        vignetteOrigin.intensity = 0.55f;
        vignetteOrigin.smoothness = 0.8f;
        vignetteOrigin.color = new Color(0.07f, 0, 0.14f);
        chromaticOrigin.intensity = 0.4f;

        behaviour.profile.vignette.settings = vignetteOrigin;
        behaviour.profile.chromaticAberration.settings = chromaticOrigin;        
        stopCo = VignetteIntensityChangeCo();
        
        StartCoroutine(stopCo);
    }

    public void ReturnDevilCameraEvent()
    {
        stopCo = VignetteIntensityChangeCo();
        StopCoroutine(stopCo);

        vignetteOrigin.intensity = 0.5f;
        vignetteOrigin.smoothness = 0.7f;
        vignetteOrigin.color = Color.black;
        chromaticOrigin.intensity = 0f;

        behaviour.profile.vignette.settings = vignetteOrigin;
        behaviour.profile.chromaticAberration.settings = chromaticOrigin;        
    }

    IEnumerator VignetteColorRedChangeCo()
    {
        vignette.color = Color.red;
        vignette.smoothness = 1;
        behaviour.profile.vignette.settings = vignette;

        float timer = new float();
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            // 비네트가 빨개지는 코드
            vignette.color = Color.Lerp(vignette.color, vignetteOrigin.color, Time.deltaTime * 2.5f);
            if(vignette.smoothness > vignetteOrigin.smoothness)
            {
                vignette.smoothness -= 0.01f;  // 비네트의 농도를 크게했다 줄이는 코드
            }
            behaviour.profile.vignette.settings = vignette;


            yield return new WaitForEndOfFrame();
        }

        vignette.color = vignetteOrigin.color;
        vignette.smoothness = vignetteOrigin.smoothness;
        behaviour.profile.vignette.settings = vignette;

        yield break;
    }

    IEnumerator VignetteIntensityChangeCo()
    {
        vignette.intensity = vignetteOrigin.intensity;
        vignette.color = vignetteOrigin.color;
        behaviour.profile.vignette.settings = vignette;

        float timer = new float();
        while (true)
        {
            timer += Time.deltaTime;

            vignette.intensity = vignetteOrigin.intensity + vignetteCurve.Evaluate(timer / 2) / 10;
            behaviour.profile.vignette.settings = vignette;

            if (timer > 2.0f)
            {
                timer = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ChromaticChangeCo()
    {
        // 기존의 키값에다가 설정된 크로매틱인텐시티값을 더해줌.
        //chromaticCurve.keys[1].value = chromaticCurve.keys[1].value + chromaticOrigin.intensity;
        //chromaticCurve.keys[2].value = chromaticCurve.keys[1].value + chromaticOrigin.intensity;

        float timer = new float();
        while (timer < 1f)
        {
            timer += Time.deltaTime;

            chromatic.intensity = chromaticOrigin.intensity + chromaticCurve.Evaluate(timer);
            behaviour.profile.chromaticAberration.settings = chromatic;

            yield return new WaitForEndOfFrame();
        }

        chromatic.intensity = chromaticOrigin.intensity;
        behaviour.profile.chromaticAberration.settings = chromatic;

        yield break;
    }
}
