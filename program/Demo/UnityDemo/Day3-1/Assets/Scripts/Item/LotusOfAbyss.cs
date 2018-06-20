using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyss : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyWorrior"))
        {
            if (!other.GetComponent<ObjectBase>().isDead)
            {
                DamageNode damageNode = new DamageNode(30, this.gameObject, 0.2f, 3, 1);
                StartCoroutine(other.GetComponent<HealthBase>().NormalDamaged(damageNode));
                NoiseCameraEvent.instance.playNormalCameraEvent(0.01f);
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
    }

    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(1.0f);
        //PlayerBase.instance.GetComponent<PlayerEffect>().BlackWaveEffectVanising(this.gameObject);

        yield break;
    }
}
