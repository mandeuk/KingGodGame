using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestLaser : MonoBehaviour { 
    // 플레이어와 부딪히면 트리거를 꺼서 한번만 맞게함.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<HighPriestLaserObj>().PlayerHit();
        }
    }
}
