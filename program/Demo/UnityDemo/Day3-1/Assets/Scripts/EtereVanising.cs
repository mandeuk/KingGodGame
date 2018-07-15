﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtereVanising : MonoBehaviour {
    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
    }

    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(10.0f);
        EnergyManager.instance.ReturnEtere(this.gameObject);

        yield break;
    }
}