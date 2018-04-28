using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAttackHitVanishingProcess : MonoBehaviour {

    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
    }

    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(1.0f);
        WraithEffect.instance.HitEffectVanishing(this.gameObject);

        yield break;
    }
}
