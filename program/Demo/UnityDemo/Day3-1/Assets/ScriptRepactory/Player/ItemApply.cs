using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemApply : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Energy"))
        {
            EffectManager.instance.playEnergyApplyEffect(gameObject);
            EnergyManager.instance.returnEnergy(collision.gameObject);
            PlayerBase.instance.energy += 1;
            PlaySceneUIManager.instance.ChangeEnergyAmountText();
        }
    }
}
