using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearDoor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.MoveStage();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DoorOpen());
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        GetComponent<Collider>().isTrigger = true;
        yield break;
    }
}
