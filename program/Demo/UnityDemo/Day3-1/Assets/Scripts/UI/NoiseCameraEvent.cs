using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class NoiseCameraEvent : MonoBehaviour {
    public static NoiseCameraEvent instance = null;
    public GameObject mainCamera;
    public GameObject exMoveCamera;
    PlayerBase player;
    PostProcessingBehaviour behaviour;

    VignetteModel.Settings vignette;
    ChromaticAberrationModel.Settings chromatic;
    PostProcessingProfile postProcess;

    // Use this for initialization
    void Awake () {
        instance = this;
        player = PlayerBase.instance;
        behaviour = Camera.main.GetComponent<PostProcessingBehaviour>();

        postProcess = behaviour.profile;

        vignette = behaviour.profile.vignette.settings;
        chromatic = behaviour.profile.chromaticAberration.settings;

        behaviour.profile.vignette.settings = vignette;
    }

    private void OnEnable()
    {
        EventManager.EnemyHitEventCall += new EnemyHitEventHandler(playNormalCameraEvent);
    }

    public IEnumerator cameraHitEvent()
    {
        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1000;
        exMoveCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        postProcess.chromaticAberration.enabled = true;
        CameraEventManager.instance.ChromaticChange();
        //player.PlayerStiff();
        yield return new WaitForSecondsRealtime(0.25f);

        mainCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 1100;
        exMoveCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        transform.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = 900;
        postProcess.chromaticAberration.enabled = false;
        //postProcess.motionBlur.enabled = false;
        yield break;
    }

    public void playNormalCameraEvent(int stateNum, GameObject enemy)
    {
        StartCoroutine(cameraHitEvent());
    }

    private void OnDisable()
    {
        EventManager.EnemyHitEventCall -= new EnemyHitEventHandler(playNormalCameraEvent);
    }
}
