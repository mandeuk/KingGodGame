using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusOfAbyss : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(!other.GetComponent<Enemyhealth>().isDead)
                StartCoroutine(other.GetComponent<Enemyhealth>().NormalDamaged(30,transform.position,0.2f,4f,1));
        }
    }

    private void OnEnable()
    {
        StartCoroutine(VanishingEffect());
    }

    public IEnumerator VanishingEffect()
    {
        yield return new WaitForSeconds(1.0f);
        PlayerStatus.instance.GetComponent<PlayerEffect>().BlackWaveEffectVanising(this.gameObject);

        yield break;
    }
}
