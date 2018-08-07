using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEventManager : MonoBehaviour {
    PlayerBase player;
    PostProcessingBehaviour behaviour;

    VignetteModel.Settings vignette;
    ChromaticAberrationModel.Settings chromatic;

    public AnimationCurve chromaticCurve;
    public AnimationCurve vignetteCurve;    

    private void Awake()
    {
        
    }

    IEnumerator AttackChromatic()
    {
        chromaticCurve.Evaluate(0.1f);
        yield break;
    }
}
