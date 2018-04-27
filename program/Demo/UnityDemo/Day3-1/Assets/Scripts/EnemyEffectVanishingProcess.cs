using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectVanishingProcess : MonoBehaviour {
	// Use this for initialization
    
    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
    }

    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(1.0f);
        EnemyEffect.instance.effectVanishing(this.gameObject);

        yield break;
    }
}
