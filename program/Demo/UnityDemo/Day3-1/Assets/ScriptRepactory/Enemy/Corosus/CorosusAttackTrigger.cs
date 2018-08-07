using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorosusAttackTrigger : MonoBehaviour {
    public GameObject attacker;
    
    private void OnTriggerEnter(Collider other)
    {
        // 이거 태그들좀 어떻게 정리해야됨;;
        if (!other.CompareTag("Player") && !other.CompareTag("Wall") && !other.CompareTag("Obstacle") && !other.CompareTag("mapClearCol"))
            return;

        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerBase>().isDodge)
                return;
        }
        
        attacker.GetComponent<CorosusBase>().isBreak = true;
        // 애니메이션에서 걸러야됨.
    }
}
