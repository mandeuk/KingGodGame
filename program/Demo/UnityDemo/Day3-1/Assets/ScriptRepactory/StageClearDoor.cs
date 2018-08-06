using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearDoor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.MoveStage2();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DoorOpen());
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        GetComponent<Collider>().isTrigger = true;
        yield break;
    }
}
