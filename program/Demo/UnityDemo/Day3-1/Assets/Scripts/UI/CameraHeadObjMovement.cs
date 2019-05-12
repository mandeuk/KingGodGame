using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMoveType
{
    Normal = 1,
    EX = 2
}

public class CameraHeadObjMovement : MonoBehaviour {
    public AnimationCurve curve;

    float cameraMovePower;

    Animator anim;
    IEnumerator normalCameraIEnum;
    IEnumerator exCameraIEnum;

    float moveY;
    float frameTime;

    // Use this for initialization
    void Awake () {
        anim = PlayerBase.instance.GetComponent<Animator>();
        normalCameraIEnum = CameraMoveRoutine((int)CameraMoveType.Normal);
        exCameraIEnum = CameraMoveRoutine((int)CameraMoveType.EX);
    }

    private void OnEnable()
    {
        EventManager.CameraMoveEventCall += new CameraMoveEventHandler(CameraMoveStart);
    }

    // Update is called once per frame
    void FixedUpdate () {
        //transform.position = new Vector3(0, transform.position.y + anim.GetFloat("PlayerYPos"), 0);     

        // 리깅이 휴먼이 아니라 제너레이트라서 바디를 못찾아서 안됨.
        //transform.localPosition = new Vector3(0,anim.bodyPosition.y + 0.5f, 0.4f);


        frameTime += Time.deltaTime;

        moveY = curve.Evaluate(frameTime) * (cameraMovePower + 1) / 2;

        transform.localPosition = new Vector3(0, anim.GetFloat("PlayerYPos") + moveY, 0.4f);
    }

    IEnumerator CameraMoveRoutine(int state)
    {
        print("CameraMove3");

        while (true)
        {
            //if (frameTime > 1.0f)
            //{
            //    print("Break");

            //    break;
            //}

            frameTime += Time.deltaTime;

            moveY = curve.Evaluate(frameTime) * (state + 1) / 2;

            yield return 0;
        }    
    }

    public void CameraMoveStart(int state)
    {
        frameTime = 0;

        if (state == 1)
        {
            cameraMovePower = 1;
        }
        if(state == 2)
        {
            cameraMovePower = 2;
        }
    }

    private void OnDisable()
    {
        EventManager.CameraMoveEventCall -= new CameraMoveEventHandler(CameraMoveStart);
    }
}
